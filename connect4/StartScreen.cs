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
        string name, error;
        public NameSelection()
        {
            name = "";
            error = "";
        }
        public string Play(int number, Player? other)
        {
            Program.SetDrawScreen(() => Draw(number));
            Console.CursorVisible = true;

            while (true)
            {
                name = "";
                Program.SafeDraw();
                ReadName();
                if (isValid(name, other))
                {
                    Console.CursorVisible = false;
                    return name;
                }
                else
                    error = "Invalid name!";
            }
        }
        public void ReadName()
        {
            while(true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (((keyInfo.KeyChar >= 'a' && keyInfo.KeyChar <= 'z') || (keyInfo.KeyChar >= 'A' && keyInfo.KeyChar <= 'Z')) && name.Length < 16)
                        name += keyInfo.KeyChar;
                    else if (keyInfo.Key == ConsoleKey.Backspace && name.Length > 0)
                        name = name.Remove(name.Length - 1);
                    else if (keyInfo.Key == ConsoleKey.Enter)
                        return;
                    else if (keyInfo.Key == ConsoleKey.Escape)
                        Program.CloseProgram();
                    Program.SafeDraw();
                }
                Thread.Sleep(50);
            }
        }
        public void Draw(int number)
        {
            Console.CursorVisible = false;
            string text = "Player " + number + "'s name:";
            Console.SetCursorPosition(Console.WindowWidth / 2 - error.Length / 2, Console.WindowHeight / 2 - 3);
            Console.Write(error);
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2 - 1);
            Console.Write(text);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 1);
            Console.Write("                 ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + 1);
            Console.Write(name);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8 + name.Length, Console.WindowHeight / 2 + 1);
            Console.CursorVisible = true;
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
            Program.SetDrawScreen(() => Draw(name));

            while(true)
            {
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
                    Program.SafeDraw();
                }
                
                Thread.Sleep(50);
            }
        }
        private void Draw(string name)
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