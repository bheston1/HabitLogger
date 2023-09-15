namespace HabitLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseFunctions.CreateTable();
            Menu.ShowMenu();
        }
    }
}