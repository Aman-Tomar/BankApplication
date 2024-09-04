using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an operation is attempted on an inactive account.
    /// This exception inherits from <see cref="ApplicationException"/> and is used to indicate that
    /// the account in question is not active and therefore cannot be used for certain operations.
    /// </summary>
    public class InactiveAccountException : ApplicationException
	{
		public InactiveAccountException(string? message = null, Exception? innerException = null) : base(message, innerException)
		{
		}
	}
}
