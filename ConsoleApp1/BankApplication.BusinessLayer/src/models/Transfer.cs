using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.interfaces;

namespace BankApplication.BusinessLayer.src.models
{
    public class Transfer
    {
        public IAccount FromAccount { get; set; }
        public IAccount ToAccount { get; set; }
        public double Amount { get; set; }
        public string FromPin { get; set; }

        public Transfer()
        {

        }
        public Transfer(IAccount fromAccount, IAccount toAccount, double amount, string fromPin)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Amount = amount;
            FromPin = fromPin;
        }
    }
}
