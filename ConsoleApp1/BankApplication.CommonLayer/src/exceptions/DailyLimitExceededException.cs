using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a daily transaction limit is exceeded.
    /// This exception inherits from <see cref="ApplicationException"/> and is used to
    /// indicate that a transaction cannot be processed due to exceeding the allowed daily limit.
    /// </summary>
    public class DailyLimitExceededException : ApplicationException
    {
        public DailyLimitExceededException(string? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
