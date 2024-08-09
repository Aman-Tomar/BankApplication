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
    public class Transaction
    {
        private IAccount toAccount;
        public int TransID { get; set; }
        public IAccount FromAccount { get; set; }
        public DateTime TranDate { get; set; }
        public double Amount { get; set; }
        public TransactionStatus Status { get; set; }
        public TransactionType TransactionType { get; set; }

        public Transaction() { }
        public Transaction(IAccount fromAccount, double amount, TransactionType transactionType)
        {
            TransID = IDGenerator.GenerateId(); // ideally new generator
            FromAccount = fromAccount;
            Amount = amount;
            TranDate = DateTime.Now;
            Status = TransactionStatus.CLOSE;
            TransactionType = transactionType;
        }

       /* public Transaction(IAccount toAccount, double amount)
        {
            this.toAccount = toAccount;
            Amount = amount;
        }*/
    }
}
