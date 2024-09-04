using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.interfaces;

namespace BankApplication.DataAccessLayer
{
    /// <summary>
    /// Defines the operations for managing account data in a repository.
    /// </summary>
    public interface IAccountsRepository
    {
        /// <summary>
        /// Saves a new account to the repository.
        /// </summary>
        /// <param name="account">The account to be saved.</param>
        void Save(IAccount account);

        /// <summary>
        /// Updates an existing account in the repository.
        /// </summary>
        /// <param name="account">The account with updated information.</param>
        void Update(IAccount account);

        /// <summary>
        /// Retrieves all accounts from the repository.
        /// </summary>
        /// <returns>A list of all accounts.</returns>
        List<IAccount> GetAll();

        /// <summary>
        /// Retrieves an account by its account number.
        /// </summary>
        /// <param name="accNo">The account number of the account to retrieve.</param>
        /// <returns>The account associated with the provided account number.</returns>
        IAccount GetById(string accNo);

        /// <summary>
        /// Gets the total number of accounts in the repository.
        /// </summary>
        /// <returns>The total number of accounts.</returns>
        int GetTotalNoOfAccounts();

        /// <summary>
        /// Gets the total worth of all accounts combined.
        /// </summary>
        /// <returns>The total worth of all accounts.</returns>
        double GetTotalWorth();

        /// <summary>
        /// Gets the count of accounts grouped by account type.
        /// </summary>
        /// <returns>A dictionary with account types as keys and the count of accounts of each type as values.</returns>
        Dictionary<string, int> GetAccountCountByType();
    }
}
