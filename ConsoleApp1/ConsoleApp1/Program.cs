using BankApplication.BusinessLayer.src.factories;
using BankApplication.BusinessLayer.src.managers;
using BankApplication.BusinessLayer.src.models;
using BankApplication.BusinessLayer.src.services;
using BankApplication.BusinessLayer.src.utils;
using BankApplication.CommonLayer.src.enums;
using BankApplication.CommonLayer.src.interfaces;
using BankApplication.CommonLayer.src.models;
using BankApplication.DataAccessLayer;
using NLog;

namespace ConsoleApp1
{
    public class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        // private static Dictionary<string, IAccount> accounts = new Dictionary<string, IAccount>();
        private static AccountManagerWrapper accountManager = new AccountManagerWrapper();
        // private static ExternalTransferService externalTransferService;

        static void Main(string[] args)
        {

            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();
                ProcessChoice(choice);
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n===== Banking System Menu =====");
            Console.WriteLine("1.  Open a new account");
            Console.WriteLine("2.  Print Balance");
            Console.WriteLine("3.  Deposit money");
            Console.WriteLine("4.  Withdraw money");
            Console.WriteLine("5.  Transfer funds");
            Console.WriteLine("6.  External transfer");
            Console.WriteLine("7.  Print all transactions");
            Console.WriteLine("8. Display all transfers");
            Console.WriteLine("9. Display all withdrawals");
            Console.WriteLine("10. Display all deposits");
            Console.WriteLine("11. Get total number of accounts");
            Console.WriteLine("12. Get number of accounts by type");
            Console.WriteLine("13. Total worth of the bank");
            Console.WriteLine("14. Display Policy Info");
            Console.WriteLine("15. Display all transactions for today");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice (1-18): ");
        }

        private static void ProcessChoice(string choice)
        {
            switch (choice)
            {
                case "1": OpenNewAccount(); break;
                case "2": PrintAccountBalance(); break;
                case "3": DepositMoney(); break;
                case "4": WithdrawMoney(); break;
                case "5": TransferFunds(); break;
                case "6": ExternalTransfer(); break;
                case "7": ResultGenerator.PrintAllLogTransactions(); break;
                case "8": ResultGenerator.PrintAllTransfers(); break;
                case "9": ResultGenerator.PrintAllWithdrawals(); break;
                case "10": ResultGenerator.PrintAllDeposits(); break;
                case "11":
                    int totalAccount = ResultGenerator.GetTotalNoOfAccounts();
                    Console.WriteLine($"Total number of accounts : {totalAccount}");
                    break;
                case "12": ResultGenerator.DisplayNoOfAccTypeWise(); break;
                case "13": ResultGenerator.DispTotalWorthOfBank(); break;
                case "14": ResultGenerator.DispPolicyInfo(); break;
                case "15": ResultGenerator.PrintTransactionsForToday(); break;
                case "0": Exit(); break;
                default: Console.WriteLine("Invalid choice. Please try again."); break;
            }
        }

        private static void OpenNewAccount()
        {
            Console.WriteLine("Select Account Type (0: SAVINGS, 1: CURRENT): ");
            AccountType accType = (AccountType)int.Parse(Console.ReadLine());

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            while (name == "")
            {
                Console.Write("Enter Valid Name: ");
                name = Console.ReadLine();
            }

            Console.Write("Set Your PIN: ");
            string pin = Console.ReadLine();
            while (pin == "")
            {
                Console.Write("Enter Valid Pin: ");
                pin = Console.ReadLine();
            }

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
            }
            catch (Exception ex)
            {
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("----------------------------------------------------------------------------------");
                logger.Error(ex, "Error during Creating Account:");
            }
        }
        private static void DepositMoney()
        {
            Console.Write("Enter Account Number: ");
            string accNo = Console.ReadLine().ToUpper();
            Console.Write("Enter Amount to Deposit: ");
            double depositAmount = double.Parse(Console.ReadLine());
            try
            {
                accountManager.Deposit(accNo, depositAmount);
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine("Deposit successful!");
                Console.WriteLine("----------------------------------------------------------------------------------");

            }
            catch (Exception ex)
            {
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("----------------------------------------------------------------------------------");
                logger.Error(ex, "Error during Deposit");
            }
        }

        private static void WithdrawMoney()
        {
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
                Console.WriteLine("----------------------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("----------------------------------------------------------------------------------");
                logger.Error(ex, "Error During withdrawal");
            }
        }

        private static void TransferFunds()
        {
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
                Console.WriteLine("----------------------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("----------------------------------------------------------------------------------");
                logger.Error(ex, "Error During TransferFunds");
            }
        }

        private static void ExternalTransfer()
        {
            Console.Write("Enter From Account Number: ");
            string fromAccNo = Console.ReadLine().ToUpper();
            IAccount fromAccount = accountManager.GetAccountById(fromAccNo);
            Console.Write("Enter From Account PIN: ");
            string fromAccPin = Console.ReadLine();
            Console.Write("Enter Amount to Transfer: ");
            double transferAmount = double.Parse(Console.ReadLine());

            Console.Write("Enter External Account Number: ");
            string extToAccNo = Console.ReadLine().ToUpper();
            Console.Write("Enter External Bank Code: ");
            string extBankCode = Console.ReadLine().ToUpper();
            Console.Write("Enter External Bank Name: ");
            string extBankName = Console.ReadLine().ToUpper();

            try
            {
                ExternalTransfer extTransfer = new ExternalTransfer(fromAccount,
                                                                    new ExternalAccount(
                                                                        extToAccNo,
                                                                        extBankCode,
                                                                        extBankName
                                                                    ), transferAmount, fromAccPin);

                bool success = accountManager.TransferFunds(extTransfer);
                if (success)
                {
                    Console.WriteLine("----------------------------------------------------------------------------------");
                    Console.WriteLine("External Transfer successful!");
                    Console.WriteLine("----------------------------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("----------------------------------------------------------------------------------");
                    Console.WriteLine("External Transfer failed");
                    Console.WriteLine("----------------------------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                logger.Error(ex, "error during External Transfer");
            }
        }

        private static void Exit()
        {
            Console.WriteLine("Thank you for using our banking system. Goodbye!");
            Environment.Exit(0);
        }

        private static void PrintAccountBalance()
        {
            Console.Write("Enter Account Number: ");
            string accNo = Console.ReadLine().ToUpper();
            while(accNo == "")
            {
                Console.Write("Enter Valid Account Number: ");
                accNo = Console.ReadLine().ToUpper();
            }

            Console.WriteLine($"Account No: {accNo} |Current Balance is: {accountManager.GetAccountById(accNo).Balance}");
        }
    }
}
    
    
    
    























    
    
    
    
    
    
    /*public class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main()
        {
            AccountManagerWrapper accountManager = new AccountManagerWrapper();
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

                    case "5":
                        Console.Write("Enter From Account Number: ");
                        string FromAccNo = Console.ReadLine().ToUpper();
                        Console.Write("Enter From Account PIN: ");
                        string FromAccPin = Console.ReadLine();
                        Console.Write("Enter Amount to Transfer: ");
                        double TransferAmount = double.Parse(Console.ReadLine());

                        Console.Write("Enter External Account Number: ");
                        string extToAccNo = Console.ReadLine().ToUpper();
                        Console.Write("Enter External Bank Code: ");
                        string extBankCode = Console.ReadLine().ToUpper();
                        Console.Write("Enter External Bank Name: ");
                        string extBankName = Console.ReadLine().ToUpper();

                        try
                        {
                            ExternalTransfer extTransfer = new ExternalTransfer(FromAccNo, TransferAmount,
                                                                                new ExternalAccount(
                                                                                    extToAccNo,
                                                                                    extBankCode,
                                                                                    extBankName
                                                                                ), FromAccPin);

                            bool success = accountManager.TransferFunds(extTransfer);
                            if (success)
                            {
                                Console.WriteLine("----------------------------------------------------------------------------------");
                                Console.WriteLine("External Transfer successful!");
                                Console.WriteLine("----------------------------------------------------------------------------------");
                            }
                            else
                            {
                                Console.WriteLine("----------------------------------------------------------------------------------");
                                Console.WriteLine("External Transfer failed");
                                Console.WriteLine("----------------------------------------------------------------------------------");
                            }

                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine("External Transfer successful!");
                            // Console.WriteLine($"New Balance: {extFromAccount.Balance}");
                            Console.WriteLine("----------------------------------------------------------------------------------");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            logger.Error(ex, "error during External Transfer");
                        }
                        break;

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
    }*/
