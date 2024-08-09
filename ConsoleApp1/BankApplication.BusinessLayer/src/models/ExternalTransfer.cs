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
    public class ExternalTransfer : Transaction
    {
        public ExternalAccount ToExternalAccount { get; }
        public string FromAccountPin { get; }

        public ExternalTransfer(IAccount fromAccount, double amount, ExternalAccount toExternalAccount, string fromPin)
            : base(fromAccount, amount, TransactionType.EXTERNALTRANSFER)
        {
            ToExternalAccount = toExternalAccount;
            FromAccountPin = fromPin;
            Status = TransactionStatus.OPEN;
        }
    }
}
