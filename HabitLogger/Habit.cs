using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitLogger
{
    internal class Habit
    {
        internal int Id { get; set; }
        internal DateTime Date { get; set; }
        internal int Quantity { get; set; }
        internal string Name { get; set; }
        internal string Measurement { get; set; }
    }
}
