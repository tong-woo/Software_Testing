namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) => {e.Cancel = true;};
            Console.CursorVisible = false;
            Console.Clear();
            Board board = new Board(7, 6);
            board.Draw(0, 0);
            while(true)
            {
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
                            board.Move();
                            break;
                        case ConsoleKey.Escape:
                            CloseProgram();
                            break;
                        default:
                            break;
                    }
                    board.Draw(0, 0);
                    if (board.isFull())
                        CloseProgram();
                }
            }
        }
        static void CloseProgram()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Environment.Exit(0);
        }
    }
}