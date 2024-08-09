using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.exceptions
{
	public class InactiveAccountException : ApplicationException
	{
		public InactiveAccountException(string? message = null, Exception? innerException = null) : base(message, innerException)
		{
		}
	}
}
