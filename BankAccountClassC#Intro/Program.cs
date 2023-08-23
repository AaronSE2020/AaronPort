using System;
using BankAccount;

class Program 
{

   static void Main(string[] args) 
   {
        BankAccount account = new BankAccount(123456, "John Doe", 1000.00);

        Console.WriteLine("Welcome to the Bank Account System!");
        account.DisplayAccountInfo();

        while (true) 
        {
            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Display Account Info");
            Console.WriteLine("4. Exit");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice) 
            {
                case 1: Console.WriteLine("Enter deposit amount: ");
                        double depositAmount = Convert.ToDouble(Console.ReadLine());
                        account.Deposit(depositAmount);
                        break;
                case 2: Console.WriteLine("Enter withdrawl amount: ");
                        double withdrawalAmount = Convert.ToDouble(Console.ReadLine());
                        account.Withdrawal(withdrawalAmount);
                        break;
                case 3: account.DisplayAccountInfo();
                        break;
                case 4: Console.WriteLine("Exiting...");
                       return;
                default: Console.WriteLine("Invalid choice. Please choose again.");
                       break;
            }
        }
   }
}




