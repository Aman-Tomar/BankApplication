using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.interfaces;

namespace BankApplication.BusinessLayer.src.models
{
    /// <summary>
    /// Represents a transfer operation between two accounts.
    /// </summary>
    public class Transfer
    {
        public string FromAccountNo { get; set; }
        public string ToAccountNo { get; set; }
        public double Amount { get; set; }
        public string FromPin { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transfer"/> class.
        /// </summary>
        public Transfer()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transfer"/> class with specified details.
        /// </summary>
        /// <param name="fromAccountNo">The account number from which the transfer is made.</param>
        /// <param name="toAccountNo">The account number to which the transfer is made.</param>
        /// <param name="amount">The amount of money to be transferred.</param>
        /// <param name="fromPin">The PIN of the account from which the transfer is made.</param>
        public Transfer(string fromAccountNo, string toAccountNo, double amount, string fromPin)
        {
            FromAccountNo = fromAccountNo;
            ToAccountNo = toAccountNo;
            Amount = amount;
            FromPin = fromPin;
        }
    }
}
