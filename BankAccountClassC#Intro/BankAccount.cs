using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountClassC_Intro
{
    internal class BankAccount
    {
        private int accountNumber;
        private string accountHolderName;
        private double balance;

        public BankAccount(int accountNumber, string accountHolderName, double initialBalance)
        {
            this.accountNumber = accountNumber;
            this.accountHolderName = accountHolderName;
            this.balance = initialBalance;
        }

        public void Deposit(double amount)
        {
            if(amount > 0)
            {
                balance += amount;
                Console.WriteLine($"{amount:C} deposited. New balance: {balance:C}");
            }
            else
            {
                Console.WriteLine("Invalid deposited amount.");
            }
        }

        public void Withdraw(double amount)
        {
            if(amount > 0 && amount <= balance) 
            {
                balance -= amount;
                Console.WriteLine($"{amount:C} withdrawn. New balance: {balance:C}");
            }
            else 
            {
                Console.WriteLine("invalid withdrawn amount or insufficient balance.");
            }
        }

        public void DisplayAccountInfo() 
        {
            Console.WriteLine($"Account Number: {accountNumber}");
            Console.WriteLine($"Account Number: {accountHolderName}");
            Console.WriteLine($"Current Balance: {balance:C}");
        }
    }
}
