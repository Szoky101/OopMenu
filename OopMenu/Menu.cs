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
        public int selectedIndex = 0;
        private ConsoleColor selectedForeground = ConsoleColor.Black;
        private ConsoleColor selectedBackground = ConsoleColor.White;
        private ConsoleColor defaultForeground = ConsoleColor.White;
        private ConsoleColor defaultBackground = ConsoleColor.Black;
        public Menu(List<string> menuItems)
        {
            MenuOption = menuItems.ToArray();
        }
        public string[] MenuOption { get; set; }

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
            DrawSelectedOption();
        }


        internal void DrawSelectedOption()
        {
            for (int i = 0; i < MenuOption.Length; i++)
            {
                int menuPosX = (Console.WindowWidth - MenuOption[i].Length) / 2;
                int menuPosY = Console.WindowHeight / 2 - MenuOption.Length / 2 + i;

                Console.SetCursorPosition(menuPosX, menuPosY);
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

                Console.Write(MenuOption[i]); 
                Console.ResetColor();

            }
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
