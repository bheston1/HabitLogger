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
            string date = Helpers.GetDateInput();
            string name = Helpers.GetHabitName("\nEnter habit name: ");
            string measurement = GetHabitMeasurement();
            int quantity = Helpers.GetNumberInput("\nEnter quantity: ");
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

        internal static void DeleteRecord()
        {
            ViewRecords();
            var recordId = Helpers.GetNumberInput("Enter ID of record to delete: ");
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM Habits WHERE Id = {recordId}";
                command.ExecuteNonQuery();
                connection.Close();
            }
            Menu.ShowMenu();
        }

        internal static void UpdateRecord()
        {
            ViewRecords();
            var recordId = Helpers.GetNumberInput("Enter ID of record to update: ");
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var checkCommand = connection.CreateCommand();
                checkCommand.CommandText = $"SELECT EXISTS(SELECT 1 FROM Habits WHERE Id = {recordId})";
                int checkQuery = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (checkQuery == 0)
                {
                    Console.WriteLine($"Record with ID {recordId} does not exist");
                    connection.Close();
                    UpdateRecord();
                }
                int quantity = Helpers.GetNumberInput("\nEnter new quantity: ");
                var command = connection.CreateCommand();
                command.CommandText = $"UPDATE Habits SET Quantity = {quantity} WHERE Id = {recordId}";
                command.ExecuteNonQuery();
                connection.Close();
            }
            Menu.ShowMenu();
        }

        internal static void ViewRecords()
        {
            Console.Clear();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Habits";
                List<Habit> records = new();
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        records.Add(new Habit
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(reader.GetString(1), "MM/dd/yyyy", new CultureInfo("en-US")),
                            Name = reader.GetString(2),
                            Measurement = reader.GetString(3),
                            Quantity = reader.GetInt32(4),
                        });
                    }
                }
                else
                {
                    Console.WriteLine("No records found");
                }
                connection.Close();
                Console.WriteLine("--------------------------------------------------------------------------------------------");
                foreach (var record in records)
                {
                    Console.WriteLine($"{record.Id}. {record.Date.ToString("MM/dd/yyyy")} - {record.Name} - {record.Measurement}: {record.Quantity}");
                }
                Console.WriteLine("--------------------------------------------------------------------------------------------");
            }
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
    }
}
