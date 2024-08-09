using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.BusinessLayer.src.factories;
using BankApplication.BusinessLayer.src.models;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.models;
using BankApplication.DataAccessLayer;

namespace BankApplication.BusinessLayer.src.utils
{
    public class ResultGenerator
    {
        public static void PrintAllLogTransactions()
        {
            var transactions = TransactionLog.GetTransactions();
            foreach (var transaction in transactions)
            {
                string accNo = transaction.Key;
                var transactionMap = transaction.Value;
                Console.WriteLine($"Account ID: {accNo}");
                PrintTransactionsByType(transactionMap);
            }
        }

        public static void PrintAllLogTransactions(string accNo)
        {
            var transactionMap = TransactionLog.GetTransactions(accNo);
            if (transactionMap == null)
            {
                Console.WriteLine("No transactions found for the given Account Number.");
                return;
            }
            Console.WriteLine($"Account ID: {accNo}");
            PrintTransactionsByType(transactionMap);
        }

        public static void PrintAllLogTransactions(TransactionType transactionType)
        {
            var transactions = TransactionLog.GetTransactions();
            foreach (var transaction in transactions)
            {
                string accNo = transaction.Key;
                var transactionMap = transaction.Value;
                if (transactionMap.ContainsKey(transactionType))
                {
                    Console.WriteLine($"Account ID: {accNo} - Transaction Type: {transactionType}");
                    foreach (var transactionDetail in transactionMap[transactionType])
                    {
                        PrintTransaction(transactionDetail);
                    }
                }
            }
        }

        public static void GetTotalNoOfAccounts()
        {
            AccountsDbRepository accountsDbRepository = new AccountsDbRepository();
            Console.WriteLine($"Total number of accounts: {accountsDbRepository.GetTotalNoOfAccounts()}");
        }

        public static void DisplayNoOfAccTypeWise()
        {
            AccountsDbRepository accountsDbRepository = new AccountsDbRepository();
            var accountsCount = accountsDbRepository.GetAccountCountByType();
            Console.WriteLine("Account Type No of Accounts");
            foreach (var account in accountsCount)
            {
                Console.WriteLine($"{account.Key} {account.Value}");
            }
        }

        public static void DispTotalWorthOfBank()
        {
            AccountsDbRepository accountsDbRepository = new AccountsDbRepository();
            double totalBalance = accountsDbRepository.GetTotalWorth();
            Console.WriteLine($"Total balance available : Rs {totalBalance:N2}");
        }

        public static void DispPolicyInfo()
        {
            var policies = PolicyFactory.Instance.GetPolicies();
            Console.WriteLine("Policy Type \t\tMinimum Balance \t\tRateOfInterest");
            foreach (var policy in policies)
            {
                Console.WriteLine($"{policy.Key} \t{policy.Value.GetMinBalance()} \t{policy.Value.GetRateOfInterest()}");
            }
        }

        public static void PrintAllTransfers()
        {
            PrintAllLogTransactions(TransactionType.TRANSFER);
        }

        public static void PrintAllWithdrawals()
        {
            PrintAllLogTransactions(TransactionType.WITHDRAW);
        }

        public static void PrintAllDeposits()
        {
            PrintAllLogTransactions(TransactionType.DEPOSIT);
        }

        public static void PrintAllExternalTransfers()
        {
            PrintAllLogTransactions(TransactionType.EXTERNALTRANSFER);
        }


        public static void PrintTransactionsForToday()
        {
            var transactions = TransactionLog.GetTransactions();
            DateTime today = DateTime.Today;
            Console.WriteLine("From \t\tTo \t\tDate \t\tAmount");
            foreach (var entry in transactions)
            {
                string accNo = entry.Key;
                var transMap = entry.Value;
                foreach (var transList in transMap.Values)
                {
                    foreach (var transaction in transList)
                    {
                        if (transaction.TranDate.Date == today)
                        {
                            PrintTransaction(transaction);
                        }
                    }
                }
            }
        }

        private static void PrintTransactionsByType(Dictionary<TransactionType, List<Transaction>> transMap)
        {
            foreach (var entry in transMap)
            {
                TransactionType type = entry.Key;
                List<Transaction> transactions = entry.Value;
                Console.WriteLine($"Transaction Type: {type}");
                Console.WriteLine("From \t\tTo \t\tDate \t\tAmount");
                foreach (var transaction in transactions)
                {
                    PrintTransaction(transaction);
                }
            }
        }

        private static void PrintTransaction(Transaction transaction)
        {
            Console.WriteLine($"{((Account)transaction.FromAccount).AccNo} \t{(transaction is ExternalTransfer ? ((ExternalTransfer)transaction).ToExternalAccount.AccNo : "N/A")} \t{transaction.TranDate} \t{transaction.Amount}");
        }
    }
}
