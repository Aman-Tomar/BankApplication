using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    internal class InsufficientBalanceException : ApplicationException
    {
        public InsufficientBalanceException(string? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
