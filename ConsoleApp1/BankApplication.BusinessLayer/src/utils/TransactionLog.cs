using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.exceptions;
using BankApplication.CommonLayer.src.models;

namespace BankApplication.BusinessLayer.src.utils
{
    public static class TransactionLog
    {
        private static Dictionary<string, Dictionary<TransactionType, List<Transaction>>> transactionLogs = new Dictionary<string, Dictionary<TransactionType, List<Transaction>>>();

        public static Dictionary<string, Dictionary<TransactionType, List<Transaction>>> GetTransactions()
        {
            if (transactionLogs != null)
            {
                return transactionLogs;
            }
            throw new TransactionNotFoundException("Transaction log is empty.");
        }

        public static Dictionary<TransactionType, List<Transaction>> GetTransactions(string accNo)
        {
            if (transactionLogs.TryGetValue(accNo, out var transactions))
            {
                return transactions;
            }
            throw new TransactionNotFoundException("No transaction found for given account.");
        }

        public static List<Transaction> GetTransactions(string accNo, TransactionType transactionType)
        { 
            if (transactionLogs.TryGetValue(accNo, out var transactions) && transactions.TryGetValue(transactionType, out var transactionList))
            {
                return transactionList;
            }
            return new List<Transaction>();
        }

        /*public static List<Transaction> GetTransactions(TransactionTypes transactionType, TransactionStatus transactionStatus)
        {
            if (transactionLogs.TryGetValue(transactionType, out var transactions) && transactions.TryGetValue(transactionStatus, out var transactionList))
            {
                return transactionList;
            }
            throw new TransactionNotFoundException("No transaction found for given type and status");
        }*/

        public static void LogTransaction(string accNo, Transaction transaction)
        {
            if (!transactionLogs.ContainsKey(accNo))
            {
                transactionLogs[accNo] = new Dictionary<TransactionType, List<Transaction>>();
            }
            if (!transactionLogs[accNo].ContainsKey(transaction.TransactionType))
            {
                transactionLogs[accNo][transaction.TransactionType] = new List<Transaction>();
            }

            transactionLogs[accNo][transaction.TransactionType].Add(transaction);
        }

        internal static void LogTransaction(string accNo, object dEPOSIT, Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
