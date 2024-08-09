using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.interfaces
{
    public interface IExternalBankService
    {
        bool Deposit(string accNo, double amount);
    }
}
