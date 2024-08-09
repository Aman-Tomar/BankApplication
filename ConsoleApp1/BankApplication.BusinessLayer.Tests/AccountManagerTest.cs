using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.BusinessLayer.src.managers;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.exceptions;
using BankApplication.CommonLayer.src.models;

namespace BankApplication.BusinessLayer.Tests
{
    public class AccountManagerTest
    {
        private AccountManager target = null;

        [TestInitialize]
        public void Init()
        {
            target = new AccountManager();
        }

        [TestMethod]
        public void CreateAccount_WithVaildInputs_ShouldCreateAccountSuccessfully()
        {
            // Arrange
            string name = "Test Name";
            string pin = "1234";
            double balance = 100000.0;
            PrivilegeType privilegeType = PrivilegeType.GOLD;
            AccountType accountType = AccountType.SAVINGS;

            // Act
            var result = target.CreateAccount(name, pin, balance, privilegeType, accountType);

            // Assert
            Assert.AreEqual(name, ((Account)result).Name);
            Assert.AreEqual(pin, ((Account)result).Pin);
            Assert.AreEqual(balance, ((Account)result).Balance);
        }

        [TestMethod, ExpectedException(typeof(MinBalanceNeedsToBeMaintainedException))]
        public void CreateAccount_WithInsufficientInitialBalance_ShouldThrowException()
        {
            string name = "Test Name";
            string pin = "1234";
            double balance = 50.0;
            PrivilegeType privilegeType = PrivilegeType.REGULAR;
            AccountType accountType = AccountType.CURRENT;

            var result = target.CreateAccount(name, pin, balance, privilegeType, accountType);
        }

        [TestMethod]
        public void Deposit_WithValidInputs_ShouldIncreaseBalance()
        {
            var account = new SavingAccount { AccNo = "12345", Balance = 1000.0 };
            double depositAmount = 500.0;
            
            var result = target.Deposit(account, depositAmount);

            Assert.IsTrue(result);
            Assert.AreEqual(1500.0, account.Balance);
        }

        [TestMethod, ExpectedException(typeof(AccountDoesNotExistException))]
        public void Deposit_WithNullAccount_ShouldThrowException()
        {

        }
    }
}
