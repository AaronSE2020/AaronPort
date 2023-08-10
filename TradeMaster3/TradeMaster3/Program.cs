using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Timers;
using System.Diagnostics;

class Program
{
    static bool stopTicker = false;
   
    static bool messageBoxVisible = false;
    private static System.Timers.Timer messageTimer;

    static void Main(string[] args)
    {
        string logo = @"
   _____ _                 _       _
  / ____(_)               | |     | |
 | |     _ _ __ ___  _ __ | | __ _| |_ ___  _ __
 | |    | | '_ ` _ \| '_ \| |/ _` | __/ _ \| '__|
 | |____| | | | | | | |_) | | (_| | || (_) | |
  \_____|_|_| |_| |_| .__/|_|\__,_|\__\___/|_|
                    | |
                    |_|
Welcome to Trademaster - Your Crypto Trading Companion
";

        Console.WriteLine(logo);

        string connectionString = "Data Source=AARON-KING\\SQLEXPRESS;Initial Catalog=TradeMaster;Integrated Security=True;Pooling=False;";

        Thread tickerThread = new Thread(() => RunCryptoTicker());
        tickerThread.Start();

        messageTimer = new System.Timers.Timer(5 * 1000); // 5 seconds in milliseconds
        messageTimer.Elapsed += MessageTimerElapsed;
        messageTimer.Start();

        while (!stopTicker)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. View Crypto Trades");
            Console.WriteLine("2. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayCryptoTrades(connectionString);
                    break;
                case "2":
                    StopProgram();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }

        tickerThread.Join();
    }

    static void DisplayCryptoTrades(string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sqlQuery = "SELECT T.TradeID, T.CryptoSymbol, T.TradeType, T.Amount, T.PricePerUnit, T.TradeDate, " +
                              "B.BrokerName, B.BrokerLocation " +
                              "FROM CryptoTrades T " +
                              "JOIN CryptoBrokers B ON T.BrokerID = B.BrokerID";

            List<string> tradeData = new List<string>();

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string data = string.Format("TradeID: {0}, CryptoSymbol: {1}, TradeType: {2}, Amount: {3}, PricePerUnit: {4}, TradeDate: {5}, BrokerName: {6}, BrokerLocation: {7}",
                                                reader["TradeID"], reader["CryptoSymbol"], reader["TradeType"], reader["Amount"], reader["PricePerUnit"],
                                                reader["TradeDate"], reader["BrokerName"], reader["BrokerLocation"]);
                    tradeData.Add(data);
                }
            }

            foreach (var data in tradeData)
            {
                Console.WriteLine(data);
            }
        }
    }

    static void RunCryptoTicker()
    {
        string[] cryptoTickers = { "BTC: $40,000", "ETH: $2,800", "ADA: $1.50", "XRP: $0.80", "DOGE: $0.30" };
        int tickerIndex = 0;

        while (!stopTicker)
        {
            if (!messageBoxVisible)
            {
                Console.Title = "Crypto Price Ticker";
                Console.SetCursorPosition(0, Console.CursorTop); // Move cursor to the beginning of the line
                Console.Write($"Crypto Price Ticker: {cryptoTickers[tickerIndex]}          ");
                tickerIndex = (tickerIndex + 1) % cryptoTickers.Length;
            }

            Thread.Sleep(1000);
        }
    }

    static void MessageTimerElapsed(object sender, ElapsedEventArgs e)
    {
        if (!messageBoxVisible)
        {
            DisplayMessageBox();
        }
    }

    static void DisplayMessageBox()
    {
        messageBoxVisible = true;
        Console.Clear();
        Console.WriteLine("Important Message!");
        Console.WriteLine("1. Open new console");
        Console.WriteLine("2. Ignore");
        Console.WriteLine("3. Close");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.D1:
                OpenNewConsole();
                break;
            case ConsoleKey.D2:
                messageBoxVisible = false;
                break;
            case ConsoleKey.D3:
                StopProgram();
                break;
        }
    }

    static void OpenNewConsole()
    {
        // Use System.Diagnostics.Process to start a new console process
        Process.Start("cmd.exe");
        messageBoxVisible = false;
    }

    static void StopProgram()
    {
        stopTicker = true;
        messageTimer.Stop();
        Console.WriteLine("\nGoodbye!");
    }
}
