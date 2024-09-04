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
    /// <summary>
    /// Defines the operations for managing transaction data in a repository.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Saves a new transaction to the repository.
        /// </summary>
        /// <param name="transaction">The transaction to be saved.</param>
        void Save(Transaction transaction);

        /// <summary>
        /// Retrieves all transactions from the repository.
        /// </summary>
        /// <returns>A list of all transactions.</returns>
        List<Transaction> GetAll();

        /// <summary>
        /// Retrieves a transaction by its transaction ID.
        /// </summary>
        /// <param name="transId">The transaction ID of the transaction to retrieve.</param>
        /// <returns>The transaction associated with the provided transaction ID.</returns>
        Transaction GetById(int transId);

        /// <summary>
        /// Retrieves transactions of a specific type.
        /// </summary>
        /// <param name="transactionType">The type of transactions to retrieve.</param>
        /// <returns>A list of transactions matching the specified type.</returns>
        List<Transaction> GetByType(TransactionType transactionType);
    }
}
