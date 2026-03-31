using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace work
{


    internal class Program
    {
        delegate void pointer();
        interface IMyBankOptions
        {
            void BankingOptions();//progress options in banking (Deposit, Withdraw, Transfare)

        }
        interface IMyBankDeposits
        {
            void Deposit();//method for making deposits
            void ExternalAccountDeposit();//method for making deposits to an external account
            void MyAccountDeposit();//method for making deposits to the user's own account
        }
        interface IMyBankWithdrawals
        {
            void Withdraw();//method for making withdrawals
        }
        interface IMyBankTransfare
        {
            void Transfer();
        }
        interface IMyBankBalance
        {
            void Balance();
        }

        //inheriting interfaces to have access to methods
        class Bank : IMyBankOptions, IMyBankDeposits, IMyBankBalance, IMyBankWithdrawals, IMyBankTransfare
        {
            public string option;//variable for the option selected by the user to select a banking option (Deposit, Withdraw, Transfare)
            private string accountNumber;//variable for the account number to be used for deposits or withdrawals
            private double amount;//variable for the amount to be deposited or withdrawn
            private double balance = 10000;//initial balance for the account


            //method for checking available balance
            //Contains pointers pointing to methods(Deposit, Withdraw, Transfare, BaankingOptions=>Back)
            public void Balance()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Available Balance R{balance}");//View current balance
                Console.WriteLine("\n 1. Deposit" +
                                  "\n 2. Withdraw" +
                                  "\n 3. Transfare " +
                                  "\n 4. Back"
                                  );

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n                           Option: ");//space of 27
                option = Console.ReadLine();
                try
                {
                    if(option == "1")
                    {
                        Console.Clear();
                        pointer op = Deposit; //pointer to deposit options
                        op();
                    }
                    else if(option == "2")
                    {
                        Console.Clear();
                        pointer op = Withdraw;
                        op();
                    }
                    else if(option == "3")
                    {
                        Console.Clear();
                        pointer op = Transfer;
                        op();
                    }
                    else if (option == "4")
                    {
                        Console.Clear();
                        pointer op = BankingOptions;
                        op();
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("**********************Something Went Wrong*********************8");
                    pointer op = Balance;
                    op();
                }
            }

            //methods for making deposits from the IMyBankDeposits interface
            //method for making deposit to Personal Account, method passed to Deposit() method for selection
            public void MyAccountDeposit()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"\nCurrent Balance: R{balance}");
                Console.Write("Enter Amount to Deposit: R ");
                amount = Convert.ToDouble(Console.ReadLine());
                balance += amount;
                Console.WriteLine($"\nNew Balance: R{balance}\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("***************************Successfully Deposited...********************************\n");

                pointer op = PROGRESS;
                op();
              
            }


            //method for making deposit to external account, method passed to Deposit() method for selection
            public void ExternalAccountDeposit()
            {
               
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Making Deposit to Account Number: {accountNumber}\n");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter Amount to Deposit: R ");
                    amount = Convert.ToDouble(Console.ReadLine());

                    Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"***************************Successfully Deposited to {accountNumber}********************************\n");

                    Console.WriteLine($"Deposited Amount: R{amount}\n");
                   

                pointer op = PROGRESS;
                op();


            }

            //method for chosing options to make deposit (My Account, External Account, Back)
            public void Deposit()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n**********Choose Deposit Option**********");
               
                Console.WriteLine("\n 1. My Account" +
                                  "\n 2. External Account" +
                                  "\n 3. Back\n"
                    );//Deposit options (My Account, External Account, Back)
                Console.Write("                              Option: ");//space of 30
                option = Console.ReadLine();

                try
                {
                    if (option == "1")
                    {
                        Console.Clear();
                       pointer op = MyAccountDeposit;//pointing to the method for making deposits to the user's own account if the user selects option 1 (My Account)
                        op();

                    }
                    else if (option == "2")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.Write("Enter Account Number: ");
                        accountNumber = Console.ReadLine();

                        while(accountNumber.Length != 10 || !accountNumber.All(char.IsDigit))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n****Invalid Account Number****\n");

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Enter Account Number: ");
                            accountNumber = Console.ReadLine();
                        }

                        Console.Clear();
                        pointer op = ExternalAccountDeposit;//pointing to the method for making deposits to an external account if the user selects option 2 (External Account)
                        op();
                    }
                    else if (option == "3")
                    {
                        Console.Clear();
                        pointer op = BankingOptions;//pointing to the banking options method if the user wants to go back
                        op();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("\n************************Invalid Option************************\n");//throwing an exception for invalid option
                    }
                }
                catch (Exception error)
                {
                    Console.Clear();
                    Console.WriteLine(error.Message);
                    pointer op = Deposit;//pointing back to the deposit method if user enters an invalid option
                    op();

                }
            }

         
            
            //method for closing the bank
            public void Close()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\n\n*************************Thank you for banking with us***************************\n\n\n");
                
            }

            //method for banking options (Deposit, Withdraw, Transfare) from the IMyBankOptions interface
            public void BankingOptions()
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                //Banking options (Deposit, Withdraw, Transfare, View Balance, EXIT)
                Console.WriteLine("   ************Select Option to continue************\n");
                Console.WriteLine("1. Deposit              2. Withdraw ");
                Console.WriteLine("3. Transfare            4. View Balance ");
                Console.WriteLine("              5. EXIT");

                Console.ForegroundColor = ConsoleColor.White;
                   
                Console.Write("\n              Option: "); //space of 14
                option = Console.ReadLine();

                try
                {
                    if (option == "1")
                    {
                        Console.Clear();
                        pointer op = Deposit; //pointing to the deposit method if the user selects option 1 (Deposit)
                        op();
                    }
                    else if (option == "2")
                    {
                        Console.Clear();
                        pointer op = Withdraw;
                        op();
                    }
                    else if (option == "3")
                    {
                        Console.Clear();
                        pointer op = Transfer;
                        op();
                    }
                    else if (option == "4")
                    {
                        Console.Clear();
                        pointer op = Balance;
                        op();
                    }
                    else if (option == "5")
                    {
                        Console.Clear();
                        pointer op = Close;
                        op();

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n************************Invalid Option************************\n"); //throwing an exception for invalid option
                        pointer op = BankingOptions; //pointing to the banking options method if the user enters an invalid option to select an option
                        op();
                    }
                }
                catch (Exception error)
                {
                    Console.Clear();
                    Console.WriteLine(error.Message);
                    pointer op = BankingOptions; //pointing to the banking options method if the user enters an invalid option to select an option
                    op();
                }
            }

            //method for exiting program or going back to the beginning of the program
            public void PROGRESS()
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.WriteLine("1.EXIT" +
                                "\n2.BACK"
                            );
                Console.Write("                              Option: ");//space of 30
                option = Console.ReadLine();

                try
                {
                    if (option == "1")
                    {
                        Console.Clear();
                        pointer op = Close;//exit the program
                        op();
                    }
                    else if (option == "2")
                    {
                        Console.Clear();
                        pointer op = BankingOptions;//initial options
                        op();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("********************Invalid Option********************");
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("***************Something went wrong****************");
                    pointer op = BankingOptions;//pointing back to same method when an error occures, remains within the program
                    op();
                }
            }
            //method for making withdrawals
            public void Withdraw()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"\n Available Balance : R{balance}");
                Console.WriteLine("\n\n Choose Withdrawal Options\n");
                Console.WriteLine("1. R1000                2. R750");
                Console.WriteLine("3. R500                 4. R250");
                Console.WriteLine("5. Own Amount          6. Back");
                Console.WriteLine("          7. EXIT\n");

                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("          Option: ");//space of 30
                option = Console.ReadLine();

                try
                {
                    if(option == "1")
                    {
                        try
                        {
                            Console.Clear();
                            if (1000 > balance)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("#####You do not have enough balance for successful transaction#####\n");

                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("1. Back        2. New Withdrawal\n");
                                Console.Write("           Option: ");
                                option = Console.ReadLine();

                                try
                                {
                                    if (option == "1")
                                    {
                                        Console.Clear();
                                        pointer op = BankingOptions;
                                        op();

                                    }
                                    else if (option == "2")
                                    {
                                        Console.Clear();
                                        pointer op = Withdraw;
                                        op();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\n************Something Went Wrong****************\n");
                                        pointer op = Close;
                                        op();
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n************Something Went Wrong****************\n");
                                    pointer op = Withdraw;
                                    op();
                                }

                            }else if (1000 < balance)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("*******************Successful Withdrawal*******************\n");

                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Withdrawn Amount : R1000");
                                balance -= 1000;
                                Console.WriteLine($"Available balance: R{balance}\n\n");

                                pointer op = PROGRESS;
                                op();
                            }
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n*****************Invalid option***************\n");
                            pointer op = Withdraw;
                            op();
                        }
                    }
                    else if(option == "2")
                    {
                        try
                        {
                            Console.Clear();
                            if (750 > balance)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("#####You do not have enough balance for successful transaction#####\n");

                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("1. Back        2. New Withdrawal\n");
                                Console.Write("           Option: ");
                                option = Console.ReadLine();

                                try
                                {
                                    if (option == "1")
                                    {
                                        Console.Clear();
                                        pointer op = BankingOptions;
                                        op();

                                    }
                                    else if (option == "2")
                                    {
                                        Console.Clear();
                                        pointer op = Withdraw;
                                        op();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\n************Something Went Wrong****************\n");
                                        pointer op = Close;
                                        op();
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n************Something Went Wrong****************\n");
                                    pointer op = Withdraw;
                                    op();
                                }

                            }
                            else if (750 < balance)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("*******************Successful Withdrawal*******************\n");

                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Withdrawn Amount : R1000");
                                balance -= 750;
                                Console.WriteLine($"Available balance: R{balance}\n\n");

                                pointer op = PROGRESS;
                                op();
                            }
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n*****************Invalid option***************\n");
                            pointer op = Withdraw;
                            op();
                        }
                    }
                    else if(option == "3")
                    {
                        try
                        {
                            Console.Clear();
                            if (500 > balance)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("#####You do not have enough balance for successful transaction#####\n");

                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.WriteLine("1. Back        2. New Withdrawal\n");
                                Console.Write("           Option: ");
                                option = Console.ReadLine();

                                try
                                {
                                    if (option == "1")
                                    {
                                        Console.Clear();
                                        pointer op = BankingOptions;
                                        op();

                                    }
                                    else if (option == "2")
                                    {
                                        Console.Clear();
                                        pointer op = Withdraw;
                                        op();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\n************Something Went Wrong****************\n");
                                        pointer op = Close;
                                        op();
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n************Something Went Wrong****************\n");
                                    pointer op = Withdraw;
                                    op();
                                }

                            }
                            else if (500 < balance)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("*******************Successful Withdrawal*******************\n");

                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Withdrawn Amount : R1000");
                                balance -= 500;
                                Console.WriteLine($"Available balance: R{balance}\n\n");

                                pointer op = PROGRESS;
                                op();
                            }
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n*****************Invalid option***************\n");
                            pointer op = Withdraw;
                            op();
                        }
                    }
                    else if(option == "4")
                    {
                        try
                        {
                            Console.Clear();
                            if (250 > balance)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("#####You do not have enough balance for successful transaction#####\n");

                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("1. Back        2. New Withdrawal\n");
                                Console.Write("           Option: ");
                                option = Console.ReadLine();

                                try
                                {
                                    if (option == "1")
                                    {
                                        Console.Clear();
                                        pointer op = BankingOptions;
                                        op();

                                    }
                                    else if (option == "2")
                                    {
                                        Console.Clear();
                                        pointer op = Withdraw;
                                        op();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\n************Something Went Wrong****************\n");
                                        pointer op = Close;
                                        op();
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n************Something Went Wrong****************\n");
                                    pointer op = Withdraw;
                                    op();
                                }

                            }
                            else if (250 < balance)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("*******************Successful Withdrawal*******************\n");

                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Withdrawn Amount : R1000");
                                balance -= 250;
                                Console.WriteLine($"Available balance: R{balance}\n\n");

                                pointer op = PROGRESS;
                                op();
                            }
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n*****************Invalid option***************\n");
                            pointer op = Withdraw;
                            op();
                        }
                    }
                    else if(option == "5")
                    {
                        Console.Clear();
                        
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Enter Withdrawal Amount: R ");
                            amount = Convert.ToDouble(Console.ReadLine());

                            if(amount > balance)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("#####You do not have enough balance for successful transaction#####\n");

                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.WriteLine("1. Back        2. New Withdrawal\n");
                                Console.Write("           Option: ");
                                option = Console.ReadLine();

                                try
                                {
                                    if(option == "1")
                                    {
                                        Console.Clear();
                                        pointer op = BankingOptions;
                                        op();

                                    }
                                    else if(option == "2")
                                    {
                                        Console.Clear();
                                        pointer op = Withdraw;
                                        op();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\n************Something Went Wrong****************\n");
                                        pointer op = Close;
                                        op();
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n************Something Went Wrong****************\n");
                                    pointer op = Withdraw;
                                    op();
                                }
                               
                            }
                            else if(amount < balance)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("*******************Successful Withdrawal*******************\n");

                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine($"Withdrawn Amount : R{amount}");
                                balance -= amount;
                                Console.WriteLine($"Available balance: R{balance}\n\n");

                                pointer op = PROGRESS;
                                op();

                            }

                        }
                        catch (Exception)
                        {

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n*****************Invalid option***************\n");
                            pointer op = Withdraw;
                            op();
                        }
                    }
                    else if(option == "6")
                    {
                        Console.Clear();
                        pointer op = BankingOptions;
                        op();
                    }
                    else if(option == "7")
                    {
                        Console.Clear();
                        pointer op = Close;
                        op();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Clear();
                        Console.WriteLine("\n************Something Went Wrong****************\n");
                        pointer op = Close;
                        op();
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    pointer op = Withdraw;
                    op();
                }
              
            }

            //method for making withdrawals
            public void Transfer()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"\nAvailable Balance : R {balance}\n");
                Console.WriteLine();

                try
                {

                    Console.Write("Beneficiary Account Number: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    accountNumber = Console.ReadLine();


                    while (accountNumber.Length != 10 || !accountNumber.All(char.IsDigit))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("**********Invalid account Number**********\n");

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("Beneficiary Account Number: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        accountNumber = Console.ReadLine();
                    }

                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nTransfer to #{accountNumber}#\n");

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("Enter Transfer Amount : R ");
                        amount = Convert.ToDouble(Console.ReadLine());

                        //display transfered amount to the user
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"\nTransfered Cash : R {amount}");

                        balance -= amount;
                        Console.WriteLine($"Available Balance : R{balance}\n");

                        pointer op = PROGRESS;
                        op();
                        
                    }
                    catch(Exception)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("****something went wrong****");
                        pointer op = Transfer;
                        op();

                    }


                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("*****something went wrong*****");
                    pointer op = Transfer;
                    op();
                }
            }
        }

        static void Main(string[] args)
            {
            Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("*****************Welcome To My EMO Bank*****************\n");

            Bank Banking = new Bank();//creating an instance of the Bank class
            Banking.BankingOptions();//calling the banking options method to start the program
        }

        
    }
}
