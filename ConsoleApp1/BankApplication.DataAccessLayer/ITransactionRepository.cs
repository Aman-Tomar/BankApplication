using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.interfaces;
using BankApplication.CommonLayer.src.models;

namespace BankApplication.DataAccessLayer
{
    public interface ITransactionRepository
    {
        void Save(Transaction transaction);
        List<Transaction> GetAll();
        Transaction GetById(int transId);
        List<Transaction> GetByType(TransactionType transactionType);
    }
}
