namespace Connect4
{
    class GameScreen
    {
        private Player player1;
        private Player player2;
        private Player currentTurn;
        private Board board;
        public GameScreen(Player player1, Player player2, int boardWidth, int boardHeight)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.currentTurn = player1;
            this.board = new Board(boardWidth, boardHeight);
        }

        public void Play()
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
                        case ConsoleKey.LeftArrow:
                            board.SelectLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            board.SelectRight();
                            break;
                        case ConsoleKey.Enter:
                            board.Move(currentTurn);
                            if (currentTurn == player1)
                                currentTurn = player2;
                            else
                                currentTurn = player1;
                            break;
                        case ConsoleKey.Escape:
                            return;
                        default:
                            break;
                    }
                    Draw();
                    if (board.isFull())
                        return;
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
                Console.SetCursorPosition(1, 1);
                player1.Draw(currentTurn == player1);
                Console.SetCursorPosition(Console.WindowWidth - player2.name.Length - 5, 1);
                player2.Draw(currentTurn == player2);
                board.Draw(Console.WindowWidth / 2 - (board.columnAmount + 1), Console.WindowHeight / 2 - (board.rowAmount / 2 + 1));
            }
        }
    }
}