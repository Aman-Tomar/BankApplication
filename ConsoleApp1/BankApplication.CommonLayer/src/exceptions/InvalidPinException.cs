using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an invalid PIN is encountered.
    /// This exception inherits from <see cref="ApplicationException"/> and is used to indicate
    /// that the PIN provided does not match the expected format or value.
    /// </summary>
    public class InvalidPinException : ApplicationException
    {
        public InvalidPinException(string? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
