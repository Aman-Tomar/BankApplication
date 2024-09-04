using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.models
{
    /// <summary>
    /// The <see cref="ExternalAccount"/> class represents an account held at an external bank.
    /// It includes the account number, bank code, and bank name.
    /// </summary>
    public class ExternalAccount
    {
        public string AccNo { get; }
        public string BankCode { get; }
        public string BankName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalAccount"/> class with default values.
        /// </summary>
        public ExternalAccount()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalAccount"/> class with the specified account number, bank code, and bank name.
        /// </summary>
        /// <param name="accNo">The account number for the external account.</param>
        /// <param name="bankCode">The bank code for the external bank.</param>
        /// <param name="bankName">The name of the external bank.</param>
        public ExternalAccount(string accNo, string bankCode, string bankName)
        {
            AccNo = accNo;
            BankCode = bankCode;
            BankName = bankName;
        }
    }
}
