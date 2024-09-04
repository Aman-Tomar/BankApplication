using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an invalid account type is encountered.
    /// This exception inherits from <see cref="ApplicationException"/> and is used to indicate
    /// that the account type specified is not valid within the system.
    /// </summary>
    public class InvalidAccountTypeException : ApplicationException
    {
        public InvalidAccountTypeException(string? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
