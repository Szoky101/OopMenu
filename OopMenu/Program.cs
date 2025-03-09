using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace OopMenu
{
    internal class Program
    {
        static string libery = "rajzok";
        static char minta = '█';
        static char torles = ' ';
        static  string name = "";

        static void Main(string[] args)
        {
            var menuElements = new List<string> { "Létrehozás", "Szerkesztés", "Kilépés" };
            var menu = new Menu(menuElements);
            int listLength = menuElements.Count;
            int selectedIndex = 0;

            if (!Directory.Exists(libery))
                Directory.CreateDirectory(libery);

            menu.DrawMenu();

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? menuElements.Count - 1 : selectedIndex - 1;
                        menu.MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == menuElements.Count - 1) ? 0 : selectedIndex + 1;
                        menu.MoveDown();
                        break;
                    case ConsoleKey.Enter:
                        if (selectedIndex == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Adja meg a nevét: ");
                            name = Console.ReadLine();
                            if (name == "")
                            {
                                name = "Névtelen";
                            }
                            Drawing();
                            menu.DrawMenu();
                        }
                        else if (selectedIndex == 1)
                        {
                            ListDrawings();
                            menu.DrawMenu();
                        }
                        else if (selectedIndex == 2)
                        {
                            Environment.Exit(1);
                        }
                        break; ;
                }

            } while (key != ConsoleKey.Escape);
        }

        private static void Drawing(int drawingId = 0)
        {
            List<string> drawing = new List<string>();
            Console.Clear();

            using (var db = new DrawingContext())
            {
                var drawingEntity = db.Drawings.Include(d => d.Dots).FirstOrDefault(d => d.DrawingId == drawingId);
                if (drawingEntity != null)
                {
                    foreach (var dot in drawingEntity.Dots)
                    {
                        Console.SetCursorPosition(dot.X, dot.Y);
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), dot.Color);
                        Console.Write(dot.Shade);
                        drawing.Add($"{dot.X},{dot.Y},{dot.Shade},{dot.Color}");
                    }
                }
            }
            ConsoleKey key;

            do
            {
                key = Console.ReadKey(true).Key;
                Console.CursorVisible = true;

                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (Console.CursorLeft < Console.WindowWidth - 1)
                            Console.CursorLeft++;
                        if (Console.CapsLock)
                        {
                            Console.Write(minta);
                            Console.CursorLeft--;
                            drawing.Add($"{Console.CursorLeft},{Console.CursorTop},{minta},{Console.ForegroundColor}");
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (Console.CursorLeft > 0)
                            Console.CursorLeft--;
                        if (Console.CapsLock)
                        {
                            Console.Write(minta);
                            Console.CursorLeft--;
                            drawing.Add($"{Console.CursorLeft},{Console.CursorTop},{minta},{Console.ForegroundColor}");
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop > 0)
                            Console.CursorTop--;
                        if (Console.CapsLock)
                        {
                            Console.Write(minta);
                            Console.CursorLeft--;
                            drawing.Add($"{Console.CursorLeft},{Console.CursorTop},{minta},{Console.ForegroundColor}");
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Console.CursorTop < Console.WindowHeight - 1)
                            Console.CursorTop++;
                        if (Console.CapsLock)
                        {
                            Console.Write(minta);
                            Console.CursorLeft--;
                            drawing.Add($"{Console.CursorLeft},{Console.CursorTop},{minta},{Console.ForegroundColor}");
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        Console.Write(minta);
                        drawing.Add($"{Console.CursorLeft},{Console.CursorTop},{minta},{Console.ForegroundColor}");
                        Console.CursorLeft--;
                        break;
                    case ConsoleKey.Backspace:
                        Console.Write(torles);
                        drawing.Add($"{Console.CursorLeft},{Console.CursorTop},{torles},{Console.ForegroundColor}");
                        Console.CursorLeft--;
                        break;
                    case ConsoleKey.F1:
                        minta = '█';
                        break;
                    case ConsoleKey.F2:
                        minta = '▓';
                        break;
                    case ConsoleKey.F3:
                        minta = '▒';
                        break;
                    case ConsoleKey.F4:
                        minta = '░';
                        break;
                    case ConsoleKey.W:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case ConsoleKey.A:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case ConsoleKey.S:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case ConsoleKey.D:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case ConsoleKey.E:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case ConsoleKey.R:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case ConsoleKey.T:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                }
            } while (key != ConsoleKey.Escape);

            SaveDrawing(drawing);
            Console.ResetColor();
        }
        private static void SaveDrawing(List<string> drawing)
        {
            using (var db = new DrawingContext())
            {
                var newDrawing = new Drawing
                {
                    Name = name,
                    CreationDate = DateTime.Now,
                    Dots = new List<Dot>()
                };

                foreach (var line in drawing)
                {
                    var parts = line.Split(',');
                    var dot = new Dot
                    {
                        X = int.Parse(parts[0]),
                        Y = int.Parse(parts[1]),
                        Shade = parts[2],
                        Color = parts[3]
                    };
                    newDrawing.Dots.Add(dot);
                }

                db.Drawings.Add(newDrawing);
                db.SaveChanges();
            }

            Console.ReadKey();
        }



        private static void ListDrawings()
        {
            Console.Clear();
            using (var db = new DrawingContext())
            {
                var drawings = db.Drawings.Include(d => d.Dots).ToList();
                if (drawings.Count == 0)
                {
                    Console.WriteLine("Nincsenek rajzok a könyvtárban.");
                    Console.ReadKey();
                    return;
                }

                int selectedIndex = 0;
                ConsoleKey key;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Mentett rajzok (Enter: szerkesztés, Delete: törlés):");
                    ConsoleColor defaultForeground = Console.ForegroundColor;
                    ConsoleColor defaultBackground = Console.BackgroundColor;
                    ConsoleColor selectedForeground = ConsoleColor.Magenta;
                    ConsoleColor selectedBackground = ConsoleColor.White;
                    for (int i = 0; i < drawings.Count; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.ForegroundColor = selectedForeground;
                            Console.BackgroundColor = selectedBackground;
                            Console.WriteLine(drawings[i].Name);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = defaultForeground;
                            Console.BackgroundColor = defaultBackground;
                            Console.WriteLine(drawings[i].Name);
                        }
                    }

                    key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex == 0) ? drawings.Count - 1 : selectedIndex - 1;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex == drawings.Count - 1) ? 0 : selectedIndex + 1;
                            break;
                        case ConsoleKey.Enter:
                            Drawing(drawings[selectedIndex].DrawingId);
                            break;
                        case ConsoleKey.Delete:
                            db.Drawings.Remove(drawings[selectedIndex]);
                            db.SaveChanges();
                            Console.WriteLine("Rajz törölve.");
                            Console.ReadKey();
                            return;
                    }

                } while (key != ConsoleKey.Escape);
            }
        }

    }
}






