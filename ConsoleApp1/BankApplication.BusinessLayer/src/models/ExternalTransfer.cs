using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.interfaces;
using BankApplication.CommonLayer.src.models;

namespace BankApplication.BusinessLayer.src.models
{
    /// <summary>
    /// Represents an external transfer transaction, where funds are transferred from an internal account to an external account.
    /// </summary>
    public class ExternalTransfer : Transaction
    {
        public ExternalAccount ToExternalAccount { get; }
        public string FromAccountPin { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalTransfer"/> class.
        /// </summary>
        /// <param name="fromAccount">The internal account from which funds are being transferred.</param>
        /// <param name="amount">The amount of funds to be transferred.</param>
        /// <param name="toExternalAccount">The external account to which funds are being transferred.</param>
        /// <param name="fromPin">The PIN of the internal account.</param>
        public ExternalTransfer(IAccount fromAccount, ExternalAccount toExternalAccount, double amount,  string fromPin)
            : base(fromAccount, amount, TransactionType.EXTERNALTRANSFER)
        {
            ToExternalAccount = toExternalAccount;
            FromAccountPin = fromPin;
            Status = TransactionStatus.OPEN;
        }
    }
}
