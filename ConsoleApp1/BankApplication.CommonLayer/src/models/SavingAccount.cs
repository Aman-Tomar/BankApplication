using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.CommonLayer.src.utils;
using BankApplication.CommonLayer.src.enums;

namespace BankApplication.CommonLayer.src.models
{
    /// <summary>
    /// The <see cref="SavingAccount"/> class represents a savings account. It inherits from the
    /// <see cref="Account"/> class and provides specific implementations for managing a savings account.
    /// </summary>
    public class SavingAccount : Account
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SavingAccount"/> class.
        /// </summary>
        public SavingAccount() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SavingAccount"/> class with the specified
        /// account holder's name, PIN, and privilege type.
        /// </summary>
        /// <param name="name">The name of the account holder.</param>
        /// <param name="pin">The PIN for accessing the account.</param>
        /// <param name="privilegeType">The privilege type associated with the account.</param>
        public SavingAccount(string name, string pin, PrivilegeType privilegeType)
        {
            Name = name;
            Pin = pin;
            PrivilegeType = privilegeType;
            AccNo = "SAV" + IDGenerator.GenerateId();
        }

        /// <summary>
        /// Opens the savings account by setting the account status to active and recording the current date and time as the date of opening.
        /// </summary>
        /// <returns><see langword="true"/> if the account is successfully opened; otherwise, <see langword="false"/>.</returns>
        public override bool Open()
        {
            Active = true;
            DateOfOpening = DateTime.Now;
            return Active;
        }

        /// <summary>
        /// Gets the type of the account, which is "SAVING" for this class.
        /// </summary>
        /// <returns>A string indicating that this is a "SAVING" account.</returns>
        public override string GetAccType()
        {
            return "SAVING";
        }

        /// <summary>
        /// Closes the savings account by setting the account status to inactive and resetting the balance to zero.
        /// </summary>
        /// <returns><see langword="true"/> if the account is successfully closed; otherwise, <see langword="false"/>.</returns>
        public override bool Close()
        {
            Active = false;
            Balance = 0;
            return !Active;
        }
    }
}

