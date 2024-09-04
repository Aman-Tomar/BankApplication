using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an invalid deposit amount is specified.
    /// This exception inherits from <see cref="ApplicationException"/> and is used to indicate
    /// that the deposit amount provided is not valid (e.g., negative or zero).
    /// </summary>
    public class InvalidDepositAmountException : ApplicationException
    {
        public InvalidDepositAmountException(string? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
