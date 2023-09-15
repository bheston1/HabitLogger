using System;
using System.Collections.Generic;
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
    }
}
