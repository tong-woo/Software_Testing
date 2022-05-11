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
            string p1Name = p1NameSelection.Play(1, null);
            ConsoleColor p1Color = p1ColorSelection.Play(p1Name, null);

            Player p1 = new Player(p1Name, p1Color);

            string p2Name = p2NameSelection.Play(2, p1);
            ConsoleColor p2Color = p2ColorSelection.Play(p2Name, p1);

            Player p2 = new Player(p2Name, p2Color);
            return (p1, p2);
        }
    }

    class NameSelection
    {
        string error;
        public NameSelection()
        {
            error = "";
        }
        public string Play(int number, Player? other)
        {
            string name = "";

            while (true)
            {
                Console.Clear();
                Draw(number);
                Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 1);
                name = Console.ReadLine() ?? "";
                if (isValid(name, other))
                    return name;
                else
                    error = "Invalid name!";
            }
        }
        public void Draw(int number)
        {
            string text = "Player " + number + "'s name:";
            Console.SetCursorPosition(Console.WindowWidth / 2 - error.Length / 2, Console.WindowHeight / 2 - 3);
            Console.Write(error);
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2 - 1);
            Console.Write(text);
        }
        private bool isValid(string name, Player? other)
        {
            if (name.Length == 0 || name.Length > 16 || (other != null && name == other.name))
                return false;
            foreach (char c in name)
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')))
                    return false;
            return true;
        }
    }

    class ColorSelection
    {
        string error;
        int selection;
        ScreenOption[] options;
        public ColorSelection()
        {
            error = "";
            selection = 0;
            options = new ScreenOption[4];
            options[0] = new ScreenOption("Red", ConsoleColor.Red);
            options[1] = new ScreenOption("Yellow", ConsoleColor.Yellow);
            options[2] = new ScreenOption("Green", ConsoleColor.Green);
            options[3] = new ScreenOption("Blue", ConsoleColor.Blue);
        }
        public ConsoleColor Play(string name, Player? other)
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
                            if (other == null || options[selection].color != other.color)
                                return options[selection].color;
                            else
                                error = "Color cannot be the same";
                            break;
                        case ConsoleKey.Escape:
                            Program.CloseProgram();
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
                Console.SetCursorPosition(Console.WindowWidth / 2 - error.Length / 2, Console.WindowHeight / 2 - 6);
                Console.Write(error);
                
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