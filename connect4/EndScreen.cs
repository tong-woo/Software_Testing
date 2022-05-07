namespace Connect4
{
    class EndScreen
    {
        private ScreenOption[] options;
        private int selection;

        public EndScreen()
        {
            selection = 0;
            options = new ScreenOption[3];
            options[0] = new ScreenOption("New game with same players");
            options[1] = new ScreenOption("New game with new players");
            options[2] = new ScreenOption("Exit game");
        }
        public int Play()
        {
            int currentWidth = 0, currentHeight = 0;

            while(true)
            {
                // Make sure the console fixes itself on resize
                if (currentWidth != Console.WindowWidth || currentHeight != Console.WindowHeight)
                {
                    Console.Clear();
                    Draw();
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
                            return 2;
                        default:
                            break;
                    }
                    Draw();
                }
                
                Thread.Sleep(50);
            }
        }

        private void Draw()
        {
            if (Console.WindowWidth < Program.minConsoleWidth || Console.WindowHeight < Program.minConsoleHeight)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("Console window should be at least {0}x{1}", Program.minConsoleWidth, Program.minConsoleHeight);
            }
            else
            {
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