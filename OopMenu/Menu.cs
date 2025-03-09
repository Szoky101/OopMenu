using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopMenu
{
    class Menu
    {
        public int selectedIndex { get; set; }
        public static string[] MenuOption { get; set; }
        private MenuOptions menuOption;

        public Menu(List<string> menuItems)
        {
            MenuOption = menuItems.ToArray();
            menuOption = new MenuOptions(menuItems);
        }
        public void DrawEdges()
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            int startX = (Console.WindowWidth - width) / 2;
            int startY = (Console.WindowHeight - height) / 2;

            Console.SetCursorPosition(startX, startY);
            Console.Write("╔" + new string('═', width - 2) + "╗");

            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write("║" + new string(' ', width - 2) + "║");
            }

            Console.SetCursorPosition(startX, startY + height - 1);
            Console.Write("╚" + new string('═', width - 2) + "╝");
        }
        internal void DrawMenu()
        {
            DrawEdges();
            menuOption.DrawSelectedOption(MenuOption, selectedIndex);
        }

        internal void MoveDown()
        {
            if (selectedIndex < MenuOption.Length - 1)
            {
                selectedIndex++;
            }
            else
            {
                selectedIndex = 0;
            }
            DrawMenu();
        }

        internal void MoveUp()
        {
            if (selectedIndex > 0)
            {
                selectedIndex--;
            }
            else
            {
                selectedIndex = MenuOption.Length - 1;
            }
            DrawMenu();
        }
    }

   
        
}
