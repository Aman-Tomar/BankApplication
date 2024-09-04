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
    /// <summary>
    /// Manages account operations such as creation, deposits, withdrawals, and fund transfers.
    /// This class handles business logic related to account management and enforces policy constraints.
    /// </summary>
    public class AccountManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManager"/> class.
        /// Sets up the repository for account data access.
        /// </summary>
        public AccountManager()
        {
        }

        /// <summary>
        /// Creates a new account with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the account holder.</param>
        /// <param name="pin">The PIN for the account.</param>
        /// <param name="balance">The initial balance of the account.</param>
        /// <param name="privilegeType">The privilege type associated with the account.</param>
        /// <param name="accountType">The type of account to create (e.g., Saving, Current).</param>
        /// <returns>The created <see cref="IAccount"/> instance.</returns>
        /// <exception cref="MinBalanceNeedsToBeMaintainedException">Thrown if the initial balance is less than the minimum balance required.</exception>
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

            return account;
        }

        /// <summary>
        /// Deposits the specified amount into the given account.
        /// </summary>
        /// <param name="toAccount">The account to deposit into.</param>
        /// <param name="amount">The amount to deposit.</param>
        /// <returns>True if the deposit was successful, otherwise false.</returns>
        /// <exception cref="AccountDoesNotExistException">Thrown if the account does not exist.</exception>
        /// <exception cref="InvalidDepositAmountException">Thrown if the deposit amount is less than or equal to 0.</exception>
        public bool Deposit(Account toAccount, double amount)
        {
            if (toAccount == null)
            {
                throw new AccountDoesNotExistException("Account does not exist.");
            }
            if (amount <= 0)
            {
                throw new InvalidDepositAmountException("Deposit amount must be greater than 0.");
            }

            toAccount.Balance += amount;
            return true;
        }

        /// <summary>
        /// Withdraws the specified amount from the given account.
        /// </summary>
        /// <param name="fromAccount">The account to withdraw from.</param>
        /// <param name="pin">The PIN for the account.</param>
        /// <param name="amount">The amount to withdraw.</param>
        /// <returns>True if the withdrawal was successful, otherwise false.</returns>
        /// <exception cref="AccountDoesNotExistException">Thrown if the account does not exist.</exception>
        /// <exception cref="InvalidWithdrawAmountException">Thrown if the withdraw amount is less than or equal to 0.</exception>
        /// <exception cref="InactiveAccountException">Thrown if the account is inactive.</exception>
        /// <exception cref="InvalidPinException">Thrown if the PIN is incorrect.</exception>
        /// <exception cref="MinBalanceNeedsToBeMaintainedException">Thrown if the withdrawal would violate the minimum balance requirement.</exception>
        /// <exception cref="DailyLimitExceededException">Thrown if the daily limit for withdrawals is exceeded.</exception>
        public bool Withdraw(Account fromAccount, string pin, double amount)
        {
            
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

            fromAccount.Balance -= amount;
            return true;
        }

        /// <summary>
        /// Transfers funds from one account to another.
        /// </summary>
        /// <param name="transfer">The transfer details.</param>
        /// <param name="fromAccount">The account to transfer from.</param>
        /// <param name="toAccount">The account to transfer to.</param>
        /// <returns>True if the transfer was successful, otherwise false.</returns>
        /// <exception cref="InactiveAccountException">Thrown if either of the accounts is inactive.</exception>
        /// <exception cref="InvalidPinException">Thrown if the PIN is incorrect.</exception>
        /// <exception cref="MinBalanceNeedsToBeMaintainedException">Thrown if the transfer would violate the minimum balance requirement.</exception>
        /// <exception cref="DailyLimitExceededException">Thrown if the daily limit for withdrawals is exceeded.</exception>
        public bool TransferFunds(Transfer transfer, Account fromAccount, Account toAccount)
        {
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

            IPolicy policy = PolicyFactory.Instance.CreatePolicy(fromAccount.GetAccType().ToString(), fromAccount.PrivilegeType.ToString());
            if (fromAccount.Balance - amount < policy.GetMinBalance())
            {
                throw new MinBalanceNeedsToBeMaintainedException("Cannot transfer due to required minimum balance.");
            }

            double dailyLimit = AccountPrivilegeManager.GetDailyLimit(fromAccount.PrivilegeType);
            double totalWithdrawnToday = GetDailyLimitUsed(fromAccount);

            if (totalWithdrawnToday + amount > dailyLimit)
            {
                throw new DailyLimitExceededException("Daily limit exceeded.");
            }

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;
            return true;
        }

        /// <summary>
        /// Handles external fund transfers.
        /// </summary>
        /// <param name="externalTransfer">The external transfer details.</param>
        /// <param name="fromAccount">The account to transfer from.</param>
        /// <returns>True if the external transfer was successful, otherwise false.</returns>
        /// <exception cref="InactiveAccountException">Thrown if the account is inactive.</exception>
        /// <exception cref="InvalidPinException">Thrown if the PIN is incorrect.</exception>
        /// <exception cref="MinBalanceNeedsToBeMaintainedException">Thrown if the transfer would violate the minimum balance requirement.</exception>
        /// <exception cref="DailyLimitExceededException">Thrown if the daily limit for withdrawals is exceeded.</exception>
        public bool TransferFunds(ExternalTransfer externalTransfer, IAccount fromAccount)
        {
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

            IPolicy policy = PolicyFactory.Instance.CreatePolicy(fromAccount.GetAccType().ToString(), fromAccount.PrivilegeType.ToString());
            if (account.Balance - amount < policy.GetMinBalance())
            {
                throw new MinBalanceNeedsToBeMaintainedException("Cannot transfer due to required minimum balance.");
            }

            double dailyLimit = AccountPrivilegeManager.GetDailyLimit(account.PrivilegeType);
            double totalWithdrawnToday = GetDailyLimitUsed(account);

            if (totalWithdrawnToday + amount > dailyLimit)
            {
                throw new DailyLimitExceededException("Daily limit exceeded.");
            }

            return true;
        }

        /// <summary>
        /// Retrieves the total amount of funds withdrawn today from the specified account.
        /// </summary>
        /// <param name="account">The account to check.</param>
        /// <returns>The total amount of funds withdrawn today.</returns>
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
