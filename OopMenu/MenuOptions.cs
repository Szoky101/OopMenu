using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopMenu
{
    class MenuOptions
    {
        public int selectedIndex = 0;
        private ConsoleColor selectedForeground = ConsoleColor.Black;
        private ConsoleColor selectedBackground = ConsoleColor.White;
        private ConsoleColor defaultForeground = ConsoleColor.White;
        private ConsoleColor defaultBackground = ConsoleColor.Black;
        public static string[] menuOption { get; set; }

        public void  ChangeColor()
        {
            for (int i = 0; i < menuOption.Length; i++)
            {
                Console.CursorVisible = false;
                Console.WriteLine(menuOption[i]);
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
            }

        }
        
    }
}
