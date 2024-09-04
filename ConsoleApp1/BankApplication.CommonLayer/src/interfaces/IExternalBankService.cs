using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.interfaces
{
    /// <summary>
    /// Defines the contract for interacting with an external bank service. Implementing classes
    /// must provide functionality to perform operations such as depositing funds into an external
    /// bank account.
    /// </summary>
    public interface IExternalBankService
    {
        /// <summary>
        /// Deposits a specified amount into an external bank account.
        /// </summary>
        /// <param name="accNo">The account number of the external bank account where the funds will be deposited.</param>
        /// <param name="amount">The amount of money to deposit.</param>
        /// <returns><see langword="true"/> if the deposit is successful; otherwise, <see langword="false"/>.</returns>
        bool Deposit(string accNo, double amount);
    }
}
