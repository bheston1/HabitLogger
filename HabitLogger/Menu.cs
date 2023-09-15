using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitLogger
{
    internal class Menu
    {
        internal static void ShowMenu()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("Habit Logger");
                Console.WriteLine("============");
                Console.WriteLine(@"Select option:
0 - Close application
1 - View all records
2 - Add new record
3 - Update record
4 - Delete record");

                var option = Console.ReadLine();
                switch (option.Trim())
                {
                    case "0":
                        closeApp = true;
                        Environment.Exit(0);
                        break;

                    case "1":
                        DatabaseFunctions.ViewRecords();
                        break;

                    case "2":
                        DatabaseFunctions.AddRecord();
                        break;

                    case "3":

                        break;

                    case "4":
                        DatabaseFunctions.DeleteRecord();
                        break;

                    default:
                        Console.WriteLine("Invalid command. Press ENTER");
                        Helpers.PressEnter();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
