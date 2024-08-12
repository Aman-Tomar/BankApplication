﻿using BankApplication.BusinessLayer.src.managers;
using BankApplication.BusinessLayer.src.models;
using BankApplication.BusinessLayer.src.services;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.interfaces;
using BankApplication.CommonLayer.src.models;
using BankApplication.DataAccessLayer;

namespace ConsoleApp1
{
    public class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main()
        {
            AccountManagerWrapper accountManager = new AccountManagerWrapper();
            IAccount account1 = null;
            IAccount account2 = null;
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine("                          WELCOME TO BANK OF CONSILIO!!");
            Console.WriteLine("----------------------------------------------------------------------------------");
            while (true)
            {
                Console.WriteLine("\n                            Bank Application Menu:");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer Funds");
                Console.WriteLine("5. External Transfer");
                Console.WriteLine("6. Exit");
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Select Account Type (0: SAVINGS, 1: CURRENT): ");
                        AccountType accType = (AccountType)int.Parse(Console.ReadLine());

                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Set Your PIN: ");
                        string pin = Console.ReadLine();

                        Console.WriteLine("Select Privilege Type (0: REGULAR, 1: GOLD, 2: PREMIUM): ");
                        PrivilegeType privilegeType = (PrivilegeType)int.Parse(Console.ReadLine());

                        Console.Write("Enter Initial Balance: ");
                        double balance = double.Parse(Console.ReadLine());

                        try
                        {
                            IAccount newAccount = accountManager.CreateAccount(name, pin, balance, privilegeType, accType);

                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine($"Account created successfully! Account Number: {newAccount.AccNo}");
                            Console.WriteLine("----------------------------------------------------------------------------------");

                            if (account1 == null)
                            {
                                account1 = newAccount;
                            }
                            else if (account2 == null)
                            {
                                account2 = newAccount;
                            }
                            else
                            {
                                Console.WriteLine("----------------------------------------------------------------------------------");
                                Console.WriteLine("You already have two accounts. Use the existing accounts for transactions.");
                                Console.WriteLine("----------------------------------------------------------------------------------");

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            logger.Error(ex, "Error during Creating Account:");
                        }
                        break;

                    case "2":
                        Console.Write("Enter Account Number: ");
                        string accNo = Console.ReadLine().ToUpper();
                        Console.Write("Enter Amount to Deposit: ");
                        double depositAmount = double.Parse(Console.ReadLine());
                        try
                        {
                            accountManager.Deposit(accNo, depositAmount);
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine("Deposit successful!");
                            // Console.WriteLine($"New Balance: {depositAccount.Balance}");
                            Console.WriteLine("----------------------------------------------------------------------------------");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            logger.Error(ex, "Error during Deposit");
                        }
                        break;

                    case "3":
                        Console.Write("Enter Account Number: ");
                        string withdrawAccNo = Console.ReadLine().ToUpper();
                        Console.Write("Enter PIN: ");
                        string withdrawPin = Console.ReadLine();
                        Console.Write("Enter Amount to Withdraw: ");
                        double withdrawAmount = double.Parse(Console.ReadLine());

                        try
                        {
                            accountManager.Withdraw(withdrawAccNo, withdrawPin, withdrawAmount);
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine("Withdrawal successful!");
                            // Console.WriteLine($"New Balance: {withdrawAccount.Balance}");
                            Console.WriteLine("----------------------------------------------------------------------------------");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            logger.Error(ex, "Error During withdrawal");
                        }
                        break;

                    case "4":
                        Console.Write("Enter From Account Number: ");
                        string fromAccountNo = Console.ReadLine().ToUpper();
                        Console.Write("Enter To Account Number: ");
                        string toAccountNo = Console.ReadLine().ToUpper();
                        Console.Write("Enter PIN: ");
                        string fromAccountPin = Console.ReadLine();
                        Console.Write("Enter Amount to Transfer: ");
                        double transferAmount = double.Parse(Console.ReadLine());

                        try
                        {
                            Transfer transfer = new Transfer(fromAccountNo, toAccountNo, transferAmount, fromAccountPin);
                            accountManager.TransferFunds(transfer);
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine("Transfer successful!");
                            // Console.WriteLine($"New Balance of From Account: {fromAccount.Balance}");
                            // Console.WriteLine($"New Balance of To Account: {toAccount.Balance}");
                            Console.WriteLine("----------------------------------------------------------------------------------");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            logger.Error(ex, "Error During TransferFunds");
                        }
                        break;

                    /*case "5":
                        if (account1 == null && account2 == null)
                        {
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine("Create an account first.");
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            break;
                        }

                        Console.Write("Enter From Account Number: ");
                        string extFromAccNo = Console.ReadLine().ToUpper();
                        Console.Write("Enter From Account PIN: ");
                        string extFromAccPin = Console.ReadLine();
                        Console.Write("Enter Amount to Transfer: ");
                        double extTransferAmount = double.Parse(Console.ReadLine());

                        IAccount extFromAccount = (extFromAccNo == account1?.AccNo) ? account1 : (extFromAccNo == account2?.AccNo) ? account2 : null;

                        if (extFromAccount != null)
                        {
                            Console.Write("Enter External Account Number: ");
                            string extToAccNo = Console.ReadLine().ToUpper();
                            Console.Write("Enter External Bank Code: ");
                            string extBankCode = Console.ReadLine().ToUpper();
                            Console.Write("Enter External Bank Name: ");
                            string extBankName = Console.ReadLine().ToUpper();

                            try
                            {
                                ExternalTransfer extTransfer = new ExternalTransfer(extFromAccount, extTransferAmount, new ExternalAccount
                                {
                                    AccNo = extToAccNo,
                                    BankCode = extBankCode,
                                    BankName = extBankName
                                }, extFromAccPin);
                                accountManager.ExternalTransferFunds(extTransfer);
                                Console.WriteLine("----------------------------------------------------------------------------------");
                                Console.WriteLine("External Transfer successful!");
                                Console.WriteLine($"New Balance: {extFromAccount.Balance}");
                                Console.WriteLine("----------------------------------------------------------------------------------");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                logger.Error(ex, "error during External Transfer");
                            }
                        }
                        else
                        {
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine("Account not found." + extFromAccNo);
                            Console.WriteLine("----------------------------------------------------------------------------------");
                        }
                        break;*/

                    case "6":
                        Console.WriteLine("----------------------------------------------------------------------------------");
                        Console.WriteLine("Exiting application.");
                        Console.WriteLine("----------------------------------------------------------------------------------");
                        return;

                    default:
                        Console.WriteLine("----------------------------------------------------------------------------------");
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.WriteLine("----------------------------------------------------------------------------------");
                        break;
                }
            }
        }
    }
}
