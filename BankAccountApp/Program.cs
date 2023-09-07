using System;
using System.Data;
using System.Data.SqlClient;

namespace BankAccountApp
{
	class Program
	{
		static string connectionString = "Server=AARON-KING\\SQLEXPRESS;Initial Catalog=StockPicks;Integrated Security=True;";

		static void Main(String[] args)
		{
			while (true) 
			{
				Console.WriteLine("Bank Account Management System");
				Console.WriteLine("1. Retrieve Account details");
				Console.WriteLine("2. Exit");
				Console.WriteLine("Enter your choice: ");
				int choice = Convert.ToInt32(Console.ReadLine());

				switch (choice) 
				{
					case 1: RetrieveAccountDetails();
						break;
					case 2: Environment.Exit(0);
						break;

					default: 
						Console.WriteLine("Invalid choice. Please try again.");
						break;
				}
			}
		}

		static void RetrieveAccountDetails()
		{
			Console.Write("Enter Account Number: ");
			int accountNumber = Convert.ToInt32(Console.ReadLine());

			using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

			using SqlCommand command = new SqlCommand("GetAccountDetails", connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@AccountNumber", accountNumber);

			using SqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
			 Console.WriteLine($"Account Number: {reader["AccountNumber"]}, " +
				    $"Holder: {reader["AccountHolderName"]}," +
				    $"Balance: {reader["Balance"]}");
			}
		}
	}



}

