using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a transaction cannot be processed due to insufficient balance in the account.
    /// This exception inherits from <see cref="ApplicationException"/> and is used to indicate that there are not enough funds to complete a transaction.
    /// </summary>
    public class InsufficientBalanceException : ApplicationException
    {
        public InsufficientBalanceException(string? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
