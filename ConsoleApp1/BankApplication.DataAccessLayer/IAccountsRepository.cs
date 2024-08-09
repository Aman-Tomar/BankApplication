using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.interfaces;

namespace BankApplication.DataAccessLayer
{
    public interface IAccountsRepository
    {
        void Save(IAccount account);
        void Update(IAccount account);
        List<IAccount> GetAll();
        IAccount GetById(string accNo);
        long GetTotalNoOfAccounts();
        double GetTotalWorth();
        Dictionary<string, long> GetAccountCountByType();
    }
}
