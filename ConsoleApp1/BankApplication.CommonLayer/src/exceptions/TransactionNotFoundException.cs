using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a transaction cannot be found.
    /// This exception inherits from <see cref="ApplicationException"/> and is used
    /// to indicate that the specified transaction ID does not exist in the system.
    /// </summary>
    public class TransactionNotFoundException : ApplicationException
    {
        public TransactionNotFoundException(string? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
