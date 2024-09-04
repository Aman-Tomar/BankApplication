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
using BankApplication.BusinessLayer.src.models;
using System.Security.Principal;

namespace BankApplication.BusinessLayer.src.services
{
    /// <summary>
    /// Provides a wrapper around account management operations, integrating with repositories for data persistence.
    /// </summary>
    public class AccountManagerWrapper
    {
        private static IAccountsRepository accountsDbRepository;
        private static AccountManager accountManager;
        private static TransactionsDbRepository transactionsDbRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManagerWrapper"/> class.
        /// </summary>
        public AccountManagerWrapper()
        {
            accountsDbRepository = new AccountsDbRepository();
            accountManager = new AccountManager();
            transactionsDbRepository = new TransactionsDbRepository();
        }

        /// <summary>
        /// Creates a new account and saves it to the database.
        /// </summary>
        /// <param name="name">The name of the account holder.</param>
        /// <param name="pin">The PIN for the account.</param>
        /// <param name="balance">The initial balance of the account.</param>
        /// <param name="privilegeType">The privilege type of the account.</param>
        /// <param name="accountType">The type of the account (e.g., SAVING, CURRENT).</param>
        /// <returns>The created account.</returns>
        public IAccount CreateAccount(string name, string pin, double balance, PrivilegeType privilegeType, AccountType accountType)
        {
            IAccount account = accountManager.CreateAccount(name, pin, balance, privilegeType, accountType);

            accountsDbRepository.Save(account);

            return account;
        }

        /// <summary>
        /// Deposits money into the specified account and updates the database.
        /// </summary>
        /// <param name="toAccountNo">The account number where the deposit is made.</param>
        /// <param name="amount">The amount to be deposited.</param>
        /// <returns>True if the deposit was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Withdraws money from the specified account and updates the database.
        /// </summary>
        /// <param name="fromAccountNo">The account number from which the withdrawal is made.</param>
        /// <param name="pin">The PIN of the account.</param>
        /// <param name="amount">The amount to be withdrawn.</param>
        /// <returns>True if the withdrawal was successful; otherwise, false.</returns>
        public bool Withdraw(string fromAccountNo, string pin, double amount)
        {
            Account fromAccount = (Account)accountsDbRepository.GetById(fromAccountNo);

            bool success = accountManager.Withdraw(fromAccount, pin, amount);

            if (success) 
            {
                Transaction transaction = new Transaction(fromAccount, amount, TransactionType.WITHDRAW);
                TransactionLog.LogTransaction(fromAccount.AccNo, transaction);

                transactionsDbRepository.Save(transaction);
                accountsDbRepository.Update(fromAccount);
            }

            return success;
        }

        /// <summary>
        /// Transfers funds between two accounts and updates the database.
        /// </summary>
        /// <param name="transfer">The transfer details.</param>
        /// <returns>True if the transfer was successful; otherwise, false.</returns>
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

                transactionsDbRepository.Save(transaction);
                accountsDbRepository.Update(fromAccount);
                accountsDbRepository.Update(toAccount);
            }

            return success;
        }

        /// <summary>
        /// Transfers funds to an external account and updates the database.
        /// </summary>
        /// <param name="externalTransfer">The external transfer details.</param>
        /// <returns>True if the transfer was successful; otherwise, false.</returns>
        public bool TransferFunds(ExternalTransfer externalTransfer)
        {
            Account fromAccount = (Account)accountsDbRepository.GetById(externalTransfer.FromAccount.AccNo);
            ExternalAccount externalAccount = externalTransfer.ToExternalAccount;

            bool success = accountManager.TransferFunds(externalTransfer, fromAccount);

            externalTransfer.Status = TransactionStatus.OPEN;
            externalTransfer.TransactionType = TransactionType.EXTERNALTRANSFER; 
            TransactionLog.LogTransaction(fromAccount.AccNo, externalTransfer);

            transactionsDbRepository.Save(externalTransfer);

            return true;
        }

        public IAccount GetAccountById(string accountNo)
        {
            return accountsDbRepository.GetById(accountNo);
        }
    }
}
