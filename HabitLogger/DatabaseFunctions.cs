using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace HabitLogger
{
    internal class DatabaseFunctions
    {
        static string connectionString = @"Data Source=habitlogger.db";

        internal static void CreateTable()
        {
            using (var connection =  new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Habits (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, Name TEXT, Measurement TEXT, Quantity INTEGER)";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        internal static void AddRecord()
        {
            Console.Clear();
            string date = GetDateInput();
            string name = GetHabitName("\nEnter habit name: ");
            string measurement = GetHabitMeasurement();
            int quantity = GetNumberInput("\nEnter quantity: ");
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO Habits(Date, Name, Measurement, Quantity) VALUES('{date}', '{name}', '{measurement}', {quantity})";
                command.ExecuteNonQuery();
                connection.Close();
            }
            Menu.ShowMenu();
        }

        private static string GetHabitMeasurement()
        {
            Console.Write("\nEnter habit unit of measurement (e.g., glasses of water, cigarettes, etc.: ");
            string measurementInput = Console.ReadLine();
            while (string.IsNullOrEmpty(measurementInput))
            {
                Console.Write("\nEntry cannot be empty.\nTry again: ");
                measurementInput = Console.ReadLine();
            }
            return measurementInput;
        }

        private static string GetHabitName(string message)
        {
            Console.Write(message);
            string nameInput = Console.ReadLine();
            while (string.IsNullOrEmpty(nameInput))
            {
                Console.Write("\nName cannot be empty.\nEnter your name: ");
                nameInput = Console.ReadLine();
            }
            return nameInput;
        }

        private static int GetNumberInput(string message)
        {
            Console.Write(message);
            string numberInput = Console.ReadLine();
            while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
            {
                Console.WriteLine("\nInvalid number. Try again.");
                numberInput = Console.ReadLine();
            }
            int finalInput = Convert.ToInt32(numberInput);
            return finalInput;
        }

        private static string GetDateInput()
        {
            Console.Write("Enter the date (format: mm/dd/yyyy): ");
            string dateInput = Console.ReadLine();
            while (!DateTime.TryParseExact(dateInput, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.Write("Invalid date format. Try again: ");
                dateInput = Console.ReadLine();
            }
            return dateInput;
        }
    }
}
