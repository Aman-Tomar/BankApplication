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
    public class AccountFactory
    {
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
