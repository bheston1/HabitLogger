using System;
using System.Collections.Generic;
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
                command.CommandText = "CREATE TABLE IF NOT EXISTS Habits (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, Quantity INTEGER)";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
