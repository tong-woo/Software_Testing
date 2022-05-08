namespace Connect4
{
    class StartScreen
    {
        NameSelection p1NameSelection, p2NameSelection;
        ColorSelection p1ColorSelection, p2ColorSelection;
        public StartScreen()
        {
            p1NameSelection = new NameSelection();
            p2NameSelection = new NameSelection();
            p1ColorSelection = new ColorSelection();
            p2ColorSelection = new ColorSelection();
        }
        public (Player, Player) Play()
        {
            string p1Name = p1NameSelection.Play(1);
            ConsoleColor p1Color = p1ColorSelection.Play(p1Name, null);

            string p2Name = p2NameSelection.Play(2);
            ConsoleColor p2Color = p2ColorSelection.Play(p2Name, p1Color);

            Player p1 = new Player(p1Name, p1Color);
            Player p2 = new Player(p2Name, p2Color);
            return (p1, p2);
        }
    }

    class NameSelection
    {
        public string Play(int number)
        {
            string name = "";
            Console.Clear();
            while (true)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 - 1);
                Console.Write("Player {0}'s name:", number);
                Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 1);
                name = Console.ReadLine() ?? "";
                if (isValid(name))
                    return name;
                else
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 3);
                    Console.Write("Invalid name!");
                }
            }
        }
        private bool isValid(string name)
        {
            if (name.Length == 0 || name.Length > 16)
                return false;
            foreach (char c in name)
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')))
                    return false;
            return true;
        }
    }

    class ColorSelection
    {
        int selection;
        ScreenOption[] options;
        public ColorSelection()
        {
            selection = 0;
            options = new ScreenOption[4];
            options[0] = new ScreenOption("Red", ConsoleColor.Red);
            options[1] = new ScreenOption("Yellow", ConsoleColor.Yellow);
            options[2] = new ScreenOption("Green", ConsoleColor.Green);
            options[3] = new ScreenOption("Blue", ConsoleColor.Blue);
        }
        public ConsoleColor Play(string name, ConsoleColor? forbidden)
        {
            int currentWidth = 0, currentHeight = 0;

            while(true)
            {
                // Make sure the console fixes itself on resize
                if (currentWidth != Console.WindowWidth || currentHeight != Console.WindowHeight)
                {
                    Console.Clear();
                    Draw(name);
                }
                currentWidth = Console.WindowWidth;
                currentHeight = Console.WindowHeight;

                // Check if a key is pressed
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            selection -= 1;
                            if (selection < 0)
                                selection = 3;
                            break;
                        case ConsoleKey.DownArrow:
                            selection += 1;
                            if (selection > 3)
                                selection = 0;
                            break;
                        case ConsoleKey.Enter:
                            if (options[selection].color != forbidden)
                                return options[selection].color;
                            break;
                        case ConsoleKey.Escape:
                            if (options[selection].color != forbidden)
                                return options[selection].color;
                            break;
                        default:
                            break;
                    }
                    Draw(name);
                }
                
                Thread.Sleep(50);
            }
        }
        private void Draw(string name)
        {
            if (Console.WindowWidth < Program.minConsoleWidth || Console.WindowHeight < Program.minConsoleHeight)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("Console window should be at least {0}x{1}", Program.minConsoleWidth, Program.minConsoleHeight);
            }
            else
            {
                string text = name + "'s color:";
                Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2 - 4);
                Console.Write(text);

                int y = Console.WindowHeight / 2 - 2;
                for (int i = 0; i < options.Length; i++)
                {
                    int x = Console.WindowWidth / 2 - options[i].text.Length / 2;
                    Console.SetCursorPosition(x - 2, y);
                    if (i == selection)
                        Console.Write("> ");
                    else
                        Console.Write("  ");

                    options[i].Draw();

                    if (i == selection)
                        Console.Write(" <");
                    else
                        Console.Write("  ");

                    y += 2;
                }
            }
        }
    }
}