using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an account does not exist in the system.
    /// This exception inherits from <see cref="ApplicationException"/> and is used to
    /// indicate that a specified account could not be found.
    /// </summary>
    public class AccountDoesNotExistException : ApplicationException
    {
        public AccountDoesNotExistException(string? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
