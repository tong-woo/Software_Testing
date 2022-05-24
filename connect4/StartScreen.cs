namespace Connect4
{
    public class StartScreen
    {
        NameSelection p1NameSelection, p2NameSelection;
        ColorSelection p1ColorSelection, p2ColorSelection;
        private readonly IConsoleIO ConsoleIO;
        public StartScreen(IConsoleIO consoleIO)
        {
            ConsoleIO = consoleIO;
            p1NameSelection = new NameSelection(ConsoleIO);
            p2NameSelection = new NameSelection(ConsoleIO);
            p1ColorSelection = new ColorSelection(ConsoleIO);
            p2ColorSelection = new ColorSelection(ConsoleIO);
        }
        public (Player, Player) Play()
        {
            string p1Name = p1NameSelection.Play(1, null);
            ConsoleColor p1Color = p1ColorSelection.Play(p1Name, null);

            Player p1 = new Player(ConsoleIO, p1Name, p1Color);

            string p2Name = p2NameSelection.Play(2, p1);
            ConsoleColor p2Color = p2ColorSelection.Play(p2Name, p1);

            Player p2 = new Player(ConsoleIO, p2Name, p2Color);
            return (p1, p2);
        }
    }

    public class NameSelection
    {
        string name, error;
        private readonly IConsoleIO ConsoleIO;
        public NameSelection(IConsoleIO consoleIO)
        {
            ConsoleIO = consoleIO;
            name = "";
            error = "";
        }
        public string Play(int number, Player? other)
        {
            Program.SetDrawScreen(ConsoleIO, () => Draw(number));
            ConsoleIO.CursorVisible = true;

            while (true)
            {
                name = "";
                Program.SafeDraw(ConsoleIO, false);
                ReadName();
                if (isValid(name, other))
                {
                    ConsoleIO.CursorVisible = false;
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
                Program.DrawOnResize(ConsoleIO);
                if (ConsoleIO.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = ConsoleIO.ReadKey(true);
                    if (((keyInfo.KeyChar >= 'a' && keyInfo.KeyChar <= 'z') || (keyInfo.KeyChar >= 'A' && keyInfo.KeyChar <= 'Z')) && name.Length < 16)
                        name += keyInfo.KeyChar;
                    else if (keyInfo.Key == ConsoleKey.Backspace && name.Length > 0)
                        name = name.Remove(name.Length - 1);
                    else if (keyInfo.Key == ConsoleKey.Enter)
                        return;
                    else if (keyInfo.Key == ConsoleKey.Escape)
                        Program.CloseProgram(ConsoleIO);
                    Program.SafeDraw(ConsoleIO, false);
                }
                Thread.Sleep(10);
            }
        }
        public void Draw(int number)
        {
            ConsoleIO.CursorVisible = false;
            string text = "Player " + number + "'s name:";
            ConsoleIO.SetCursorPosition(ConsoleIO.WindowWidth / 2 - error.Length / 2, ConsoleIO.WindowHeight / 2 - 3);
            ConsoleIO.Write(error);
            ConsoleIO.SetCursorPosition(ConsoleIO.WindowWidth / 2 - text.Length / 2, ConsoleIO.WindowHeight / 2 - 1);
            ConsoleIO.Write(text);
            ConsoleIO.SetCursorPosition(ConsoleIO.WindowWidth / 2 - 8, ConsoleIO.WindowHeight / 2 + 1);
            ConsoleIO.Write("                 ");
            ConsoleIO.SetCursorPosition(ConsoleIO.WindowWidth / 2 - 8, ConsoleIO.WindowHeight / 2 + 1);
            ConsoleIO.Write(name);
            ConsoleIO.SetCursorPosition(ConsoleIO.WindowWidth / 2 - 8 + name.Length, ConsoleIO.WindowHeight / 2 + 1);
            ConsoleIO.CursorVisible = true;
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

    public class ColorSelection
    {
        public string error;
        int selection;
        ScreenOption[] options;
        private readonly IConsoleIO ConsoleIO;
        public ColorSelection(IConsoleIO consoleIO)
        {
            ConsoleIO = consoleIO;
            error = "";
            selection = 0;
            options = new ScreenOption[4];
            options[0] = new ScreenOption(ConsoleIO, "Red", ConsoleColor.Red);
            options[1] = new ScreenOption(ConsoleIO, "Yellow", ConsoleColor.Yellow);
            options[2] = new ScreenOption(ConsoleIO, "Green", ConsoleColor.Green);
            options[3] = new ScreenOption(ConsoleIO, "Blue", ConsoleColor.Blue);
        }
        public ConsoleColor Play(string name, Player? other)
        {
            Program.SetDrawScreen(ConsoleIO, () => Draw(name));

            while(true)
            {
                Program.DrawOnResize(ConsoleIO);
                // Check if a key is pressed
                if (ConsoleIO.KeyAvailable)
                {
                    ConsoleKey key = ConsoleIO.ReadKey(true).Key;
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
                            Program.CloseProgram(ConsoleIO);
                            break;
                        default:
                            break;
                    }
                    Program.SafeDraw(ConsoleIO, false);
                }
                
                Thread.Sleep(10);
            }
        }
        public void Draw(string name)
        {
            string text = name + "'s color:";
            ConsoleIO.SetCursorPosition(ConsoleIO.WindowWidth / 2 - error.Length / 2, ConsoleIO.WindowHeight / 2 - 6);
            ConsoleIO.Write(error);
            
            ConsoleIO.SetCursorPosition(ConsoleIO.WindowWidth / 2 - text.Length / 2, ConsoleIO.WindowHeight / 2 - 4);
            ConsoleIO.Write(text);

            int y = ConsoleIO.WindowHeight / 2 - 2;
            for (int i = 0; i < options.Length; i++)
            {
                int x = ConsoleIO.WindowWidth / 2 - options[i].text.Length / 2;
                ConsoleIO.SetCursorPosition(x - 2, y);
                if (i == selection)
                    ConsoleIO.Write("> ");
                else
                    ConsoleIO.Write("  ");

                options[i].Draw();

                if (i == selection)
                    ConsoleIO.Write(" <");
                else
                    ConsoleIO.Write("  ");

                y += 2;
            }
        }
    }
}