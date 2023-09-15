using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitLogger
{
    internal class Helpers
    {
        internal static void PressEnter()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            } while (key.Key != ConsoleKey.Enter);
        }

        internal static string GetHabitName(string message)
        {
            Console.Write(message);
            string nameInput = Console.ReadLine();
            while (string.IsNullOrEmpty(nameInput))
            {
                Console.Write("Name cannot be empty. Enter habit name: ");
                nameInput = Console.ReadLine();
            }
            return nameInput;
        }

        internal static int GetNumberInput(string message)
        {
            Console.Write(message);
            string numberInput = Console.ReadLine();
            while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
            {
                Console.Write("Invalid number. Try again: ");
                numberInput = Console.ReadLine();
            }
            int finalInput = Convert.ToInt32(numberInput);
            return finalInput;
        }

        internal static string GetDateInput()
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
