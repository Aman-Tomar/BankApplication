using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
    public class DailyLimitExceededException : ApplicationException
    {
        public DailyLimitExceededException(string? message = null, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
