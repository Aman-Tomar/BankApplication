using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.enums
{
    /// <summary>
    /// Represents the different types of financial transactions that can be performed.
    /// </summary>
    public enum TransactionType
    {
        TRANSFER,
        WITHDRAW,
        DEPOSIT,
        EXTERNALTRANSFER,
    }
}
