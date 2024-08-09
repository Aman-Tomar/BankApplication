using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.CommonLayer.src.models
{
    public class ExternalAccount
    {
        public string AccNo { get; }
        public string BankCode { get; }
        public string BankName { get; }

        public ExternalAccount()
        {

        }

        public ExternalAccount(string accNo, string bankCode, string bankName)
        {
            AccNo = accNo;
            BankCode = bankCode;
            BankName = bankName;
        }
    }
}
