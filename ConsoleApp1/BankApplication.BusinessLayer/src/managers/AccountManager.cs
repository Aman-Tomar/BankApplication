using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.BusinessLayer.src.factories;
using BankApplication.CommonLayer.src.models;
using BankApplication.BusinessLayer.src.utils;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.exceptions;
using BankApplication.CommonLayer.src.interfaces;
using BankApplication.BusinessLayer.src.models;
using BankApplication.DataAccessLayer;

namespace BankApplication.BusinessLayer.src.managers
{
    public class AccountManager
    {
        public IAccount CreateAccount(string name, string pin, double balance, PrivilegeType privilegeType, AccountType accountType)
        {
            IAccount account = AccountFactory.CreateAccount(name, pin, balance, privilegeType, accountType);
            IPolicy policy = PolicyFactory.Instance.CreatePolicy(accountType.ToString(), privilegeType.ToString());

            if (balance < policy.GetMinBalance())
            {
                throw new MinBalanceNeedsToBeMaintainedException("Initial balance is less than the minimum balance required.");
            }

            ((Account)account).Policy = policy;
            account.Open();

            AccountsDbRepository accountsDbRepository = new AccountsDbRepository();
            accountsDbRepository.Save(account);

            return account;
        }

        public bool Deposit(string toAccountNo, double amount)
        {
            // Get account details from database
            AccountsDbRepository accountsDbRepository = new AccountsDbRepository();
            Account toAccount = (Account)accountsDbRepository.GetById(toAccountNo);

            if (toAccount == null)
            {
                throw new AccountDoesNotExistException("Account does not exist.");
            }
            if (amount <= 0)
            {
                throw new InvalidDepositAmountException("Deposit amount must be greater than 0.");
            }

            Transaction transaction = new Transaction(toAccount, amount, TransactionType.DEPOSIT);
            toAccount.Balance += amount;
            TransactionLog.LogTransaction(toAccount.AccNo, transaction);

            TransactionsDbRepository transactionsDbRepository = new TransactionsDbRepository();
            transactionsDbRepository.Save(transaction);
            accountsDbRepository.Update(toAccount);

            return true;
        }

        public bool Withdraw(string fromAccountNo, string pin, double amount)
        {
            // Get account details from bank
            AccountsDbRepository accountsDbRepository = new AccountsDbRepository();
            Account fromAccount = (Account)accountsDbRepository.GetById(fromAccountNo);

            if (fromAccount == null)
            {
                throw new AccountDoesNotExistException("Account does not exist.");
            }
            if (amount <= 0)
            {
                throw new InvalidWithdrawAmountException("Withdraw amount must be greater than 0.");
            }

            if (!fromAccount.Active)
            {
                throw new InactiveAccountException("Account is inactive.");
            }
            if (fromAccount.Pin != pin)
            {
                throw new InvalidPinException("Invalid PIN.");
            }

            IPolicy policy = PolicyFactory.Instance.CreatePolicy(fromAccount.GetAccType().ToString(), fromAccount.PrivilegeType.ToString());
            if (fromAccount.Balance - amount < policy.GetMinBalance())
            {
                throw new MinBalanceNeedsToBeMaintainedException("Cannot withdraw due to required minimum balance.");
            }

            double dailyLimit = AccountPrivilegeManager.GetDailyLimit(fromAccount.PrivilegeType);
            double dailyLimitUsed = GetDailyLimitUsed(fromAccount);

            if (dailyLimitUsed + amount > dailyLimit)
            {
                throw new DailyLimitExceededException("Daily Limit Exceeded.");
            }

            Transaction transaction = new Transaction(fromAccount, amount, TransactionType.WITHDRAW);
            fromAccount.Balance -= amount;
            TransactionLog.LogTransaction(fromAccount.AccNo, transaction);

            TransactionsDbRepository transactionsDbRepository = new TransactionsDbRepository();
            transactionsDbRepository.Save(transaction);
            accountsDbRepository.Update(fromAccount);

            return true;
        }

        public bool TransferFunds(Transfer transfer)
        {
            Account fromAccount = (Account)transfer.FromAccount;
            Account toAccount = (Account)transfer.ToAccount;
            double amount = transfer.Amount;
            string fromPin = transfer.FromPin;

            if (!fromAccount.Active || !toAccount.Active)
            {
                throw new InactiveAccountException("One or both accounts are inactive.");
            }

            if (fromAccount.Pin != fromPin.ToString())
            {
                throw new InvalidPinException("Invalid PIN.");
            }

            if (fromAccount.Balance - amount < fromAccount.Policy.GetMinBalance())
            {
                throw new MinBalanceNeedsToBeMaintainedException("Cannot transfer due to required minimum balance.");
            }

            double dailyLimit = AccountPrivilegeManager.GetDailyLimit(fromAccount.PrivilegeType);
            double totalWithdrawnToday = GetDailyLimitUsed(fromAccount);

            if (totalWithdrawnToday + amount > dailyLimit)
            {
                throw new DailyLimitExceededException("Daily limit exceeded.");
            }

            Transaction transaction = new Transaction(fromAccount, amount, TransactionType.TRANSFER);
            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            TransactionLog.LogTransaction(fromAccount.AccNo, TransactionType.TRANSFER, transaction);
            TransactionLog.LogTransaction(toAccount.AccNo, TransactionType.TRANSFER, transaction);

            TransactionsDbRepository transactionsDbRepository = new TransactionsDbRepository(); 
            transactionsDbRepository.Save(transaction);

            return true;
        }

        public bool TransferFunds(ExternalTransfer externalTransfer)
        {
            IAccount fromAccount = externalTransfer.FromAccount;
            ExternalAccount externalAccount = externalTransfer.ToExternalAccount;
            double amount = externalTransfer.Amount;
            string fromPin = externalTransfer.FromAccountPin;

            Account account = (Account)fromAccount;
            if (!account.Active)
            {
                throw new InactiveAccountException("The account is inactive.");
            }
            if (account.Pin != fromPin)
            {
                throw new InvalidPinException("Invalid PIN.");
            }
            if (account.Balance - amount < account.Policy.GetMinBalance())
            {
                throw new MinBalanceNeedsToBeMaintainedException("Cannot transfer due to required minimum balance.");
            }

            double dailyLimit = AccountPrivilegeManager.GetDailyLimit(account.PrivilegeType);
            double totalWithdrawnToday = GetDailyLimitUsed(account);

            if (totalWithdrawnToday + amount > dailyLimit)
            {
                throw new DailyLimitExceededException("Daily limit exceeded.");
            }

            externalTransfer.Status = TransactionStatus.OPEN;
            TransactionLog.LogTransaction(account.AccNo, TransactionType.EXTERNALTRANSFER, externalTransfer);

            TransactionsDbRepository transactionsDbRepository = new TransactionsDbRepository();
            transactionsDbRepository.Save(externalTransfer);

            return true;
        }

        private double GetDailyLimitUsed(Account account)
        {
            double dailyLimitUsed = 0;
            List<Transaction> withdrawals = TransactionLog.GetTransactions(account.AccNo, TransactionType.WITHDRAW);

            foreach (Transaction transaction in withdrawals)
            {
                if (transaction.TranDate.Date == DateTime.Now)
                {
                    dailyLimitUsed += transaction.Amount;
                }
            }
            return dailyLimitUsed;
        }
    }
}
