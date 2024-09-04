using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.models;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.exceptions;
using BankApplication.CommonLayer.src.interfaces;

namespace BankApplication.BusinessLayer.src.factories
{
    /// <summary>
    /// A factory class for creating instances of <see cref="IAccount"/> based on specified account types.
    /// This class provides a method to create accounts with the given parameters and returns the appropriate account type.
    /// </summary>
    public class AccountFactory
    {
        /// <summary>
        /// Creates an account based on the specified account type and initializes it with the provided details.
        /// </summary>
        /// <param name="name">The name of the account holder.</param>
        /// <param name="pin">The PIN for the account.</param>
        /// <param name="balance">The initial balance for the account.</param>
        /// <param name="privilegeType">The privilege type associated with the account.</param>
        /// <param name="accountType">The type of account to create (e.g., saving, current).</param>
        /// <returns>An instance of <see cref="IAccount"/> representing the newly created account.</returns>
        /// <exception cref="InvalidAccountTypeException">Thrown when an invalid account type is specified.</exception>
        public static IAccount CreateAccount(string name, string pin, double balance, PrivilegeType privilegeType, AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.SAVING:
                    return new SavingAccount(name, pin, privilegeType) { Balance = balance };
                case AccountType.CURRENT:
                    return new CurrentAccount(name, pin, privilegeType) { Balance = balance };
                default:
                    throw new InvalidAccountTypeException("Invalid account type specified");
            }
        }
    }
}
