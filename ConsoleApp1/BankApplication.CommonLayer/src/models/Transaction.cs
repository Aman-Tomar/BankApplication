using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.utils;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.interfaces;

namespace BankApplication.CommonLayer.src.models
{
    /// <summary>
    /// The <see cref="Transaction"/> class represents a financial transaction between accounts.
    /// It includes details such as transaction ID, source account, date, amount, status, and type.
    /// </summary>
    public class Transaction
    {
        private IAccount toAccount;
        public int TransID { get; set; }
        public IAccount FromAccount { get; set; }
        public DateTime TranDate { get; set; }
        public double Amount { get; set; }
        public TransactionStatus Status { get; set; }
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        public Transaction() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class with the specified
        /// details including the source account, amount, and transaction type.
        /// </summary>
        /// <param name="fromAccount">The account from which the transaction is initiated.</param>
        /// <param name="amount">The amount involved in the transaction.</param>
        /// <param name="transactionType">The type of the transaction.</param>  
        public Transaction(IAccount fromAccount, double amount, TransactionType transactionType)
        {
            TransID = IDGenerator.GenerateId(); // ideally new generator
            FromAccount = fromAccount;
            Amount = amount;
            TranDate = DateTime.Now;
            Status = TransactionStatus.CLOSE;
            TransactionType = transactionType;
        }

        /* 
        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class with the specified
        /// destination account and amount. (Note: This constructor is currently commented out and not used.)
        /// </summary>
        /// <param name="toAccount">The account to which the transaction is directed.</param>
        /// <param name="amount">The amount involved in the transaction.</param>
        public Transaction(IAccount toAccount, double amount)
        {
            this.toAccount = toAccount;
            Amount = amount;
        }*/
    }
}
