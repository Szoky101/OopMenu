using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopMenu
{
    class MenuOptions
    {
        private List<string> menuItems;

        public MenuOptions(List<string> menuItems)
        {
            this.menuItems = new List<string>();
            foreach (var item in menuItems)
            {
                this.menuItems.Add(AddBorders(item));
            }
        }
        private ConsoleColor selectedForeground = ConsoleColor.Black;
        private ConsoleColor selectedBackground = ConsoleColor.White;
        private ConsoleColor defaultForeground = ConsoleColor.White;
        private ConsoleColor defaultBackground = ConsoleColor.Black;



        private string AddBorders(string item)
        {
            string border = new string('═', item.Length + 2);
            return $"╔{border}╗\n║ {item} ║\n╚{border}╝";
        }
        public void DrawSelectedOption(string[] menuOptions, int selectedIndex)
        {
            for (int i = 0; i < menuOptions.Length; i++)
            {
                string borderedItem = AddBorders(menuOptions[i]);
                string[] lines = borderedItem.Split('\n');
                int menuPosX = (Console.WindowWidth - lines[0].Length) / 2;
                int menuPosY = Console.WindowHeight / 2 - menuOptions.Length + i * lines.Length;

                for (int j = 0; j < lines.Length; j++)
                {
                    Console.SetCursorPosition(menuPosX, menuPosY + j);
                    Console.CursorVisible = false;

                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = selectedForeground;
                        Console.BackgroundColor = selectedBackground;
                    }
                    else
                    {
                        Console.ForegroundColor = defaultForeground;
                        Console.BackgroundColor = defaultBackground;
                    }

                    Console.WriteLine(lines[j]);
                    Console.ResetColor();
                }
            }
        }
    }
}
