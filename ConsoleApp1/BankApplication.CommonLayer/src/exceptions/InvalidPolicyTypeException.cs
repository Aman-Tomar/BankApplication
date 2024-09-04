using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an invalid policy type is encountered.
    /// This exception inherits from <see cref="ApplicationException"/> and is used to indicate
    /// that the policy type provided is not recognized or is invalid within the system.
    /// </summary>
    public class InvalidPolicyTypeException : ApplicationException
    {
        public InvalidPolicyTypeException(string? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
