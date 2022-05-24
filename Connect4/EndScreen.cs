namespace Connect4
{
    class EndScreen
    {
        private ScreenOption[] options;
        private int selection;
        private readonly IConsoleIO ConsoleIO;

        public EndScreen(IConsoleIO consoleIO) {
            ConsoleIO = consoleIO;
            selection = 0;
            options = new ScreenOption[3];
            options[0] = new ScreenOption(ConsoleIO, "New game with same players");
            options[1] = new ScreenOption(ConsoleIO, "New game with new players");
            options[2] = new ScreenOption(ConsoleIO, "Exit game");
        }
        public int Play()
        {
            Program.SetDrawScreen(ConsoleIO, Draw);

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
                                selection = 2;
                            break;
                        case ConsoleKey.DownArrow:
                            selection += 1;
                            if (selection > 2)
                                selection = 0;
                            break;
                        case ConsoleKey.Enter:
                            return selection;
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

        private void Draw()
        {
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