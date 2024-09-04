using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an attempt is made to withdraw funds
    /// below the minimum balance required to be maintained in the account.
    /// This exception inherits from <see cref="ApplicationException"/> and is used
    /// to indicate that the operation cannot be completed because it would result in
    /// the account balance falling below the minimum required threshold.
    /// </summary>
    public class MinBalanceNeedsToBeMaintainedException : ApplicationException
    {
        public MinBalanceNeedsToBeMaintainedException(string? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
