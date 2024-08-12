using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.BusinessLayer.src.managers;
using BankApplication.BusinessLayer.src.utils;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.interfaces;
using BankApplication.CommonLayer.src.models;
using BankApplication.DataAccessLayer;
using BankApplication.CommonLayer.src.models;
using BankApplication.BusinessLayer.src.models;

namespace BankApplication.BusinessLayer.src.services
{
    public class AccountManagerWrapper
    {
        private static IAccountsRepository accountsDbRepository;
        private static AccountManager accountManager;
        private static TransactionsDbRepository transactionsDbRepository;


        public AccountManagerWrapper()
        {
            accountsDbRepository = new AccountsDbRepository();
            accountManager = new AccountManager();
            transactionsDbRepository = new TransactionsDbRepository();
        }

        public IAccount CreateAccount(string name, string pin, double balance, PrivilegeType privilegeType, AccountType accountType)
        {
            IAccount account = accountManager.CreateAccount(name, pin, balance, privilegeType, accountType);

            AccountsDbRepository accountsDbRepository = new AccountsDbRepository();
            accountsDbRepository.Save(account);

            return account;
        }


        public bool Deposit(string toAccountNo, double amount)
        {
            Account toAccount = (Account)accountsDbRepository.GetById(toAccountNo);

            bool success = accountManager.Deposit(toAccount, amount);

            if (success)
            {
                Transaction transaction = new Transaction(toAccount, amount, TransactionType.DEPOSIT);
                TransactionLog.LogTransaction(toAccount.AccNo, transaction);

                transactionsDbRepository.Save(transaction);
                accountsDbRepository.Update(toAccount);
            }
            return success;
        }

        public bool Withdraw(string fromAccountNo, string pin, double amount)
        {
            Account fromAccount = (Account)accountsDbRepository.GetById(fromAccountNo);

            bool success = accountManager.Withdraw(fromAccount, pin, amount);

            if (success) 
            {
                Transaction transaction = new Transaction(fromAccount, amount, TransactionType.WITHDRAW);
                TransactionLog.LogTransaction(fromAccount.AccNo, transaction);

                TransactionsDbRepository transactionsDbRepository = new TransactionsDbRepository();
                transactionsDbRepository.Save(transaction);
                accountsDbRepository.Update(fromAccount);
            }

            return success;
        }
        public bool TransferFunds(Transfer transfer)
        {
            Account fromAccount = (Account)accountsDbRepository.GetById(transfer.FromAccountNo);
            Account toAccount = (Account)accountsDbRepository.GetById(transfer.ToAccountNo);

            bool success = accountManager.TransferFunds(transfer, fromAccount, toAccount);
            if (success)
            {
                Transaction transaction = new Transaction(fromAccount, transfer.Amount, TransactionType.TRANSFER);
                TransactionLog.LogTransaction(fromAccount.AccNo, transaction);
                TransactionLog.LogTransaction(toAccount.AccNo, transaction);

                TransactionsDbRepository transactionsDbRepository = new TransactionsDbRepository();
                transactionsDbRepository.Save(transaction);
                accountsDbRepository.Update(fromAccount);
                accountsDbRepository.Update(toAccount);
            }

            return success;
        }

        public bool TransferFunds(ExternalTransfer externalTransfer)
        {
            IAccount fromAccount = externalTransfer.FromAccount;
            ExternalAccount externalAccount = externalTransfer.ToExternalAccount;

            bool success = accountManager.TransferFunds(externalTransfer, fromAccount);

            return true;
        }
    }
}
