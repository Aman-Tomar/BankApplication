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
        public string FromAccountNo { get; set; }
        public string ToAccountNo { get; set; }
        public double Amount { get; set; }
        public string FromPin { get; set; }

        public Transfer()
        {

        }
        public Transfer(string fromAccountNo, string toAccountNo, double amount, string fromPin)
        {
            FromAccountNo = fromAccountNo;
            ToAccountNo = toAccountNo;
            Amount = amount;
            FromPin = fromPin;
        }
    }
}
