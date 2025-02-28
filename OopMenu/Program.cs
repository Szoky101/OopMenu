using System.Security.Principal;

namespace OopMenu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var menuOptions = new List<string> {"Létrehozás", "Szerkesztés", "Törlés","Világ", "Vége"};
            var menu = new Menu(menuOptions);
            while (true)
            {
                menu.DrawMenu();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        menu.MoveDown();
                        break;
                    case ConsoleKey.UpArrow:
                        menu.MoveUp();
                        break;
                    case ConsoleKey.Escape:
                        System.Environment.Exit(1);

                        break;

                }
            }
        }
    }
}
