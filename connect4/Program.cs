namespace Connect4
{
    class Program
    {
        public static ConsoleColor defaultColor = ConsoleColor.White;
        public static int nameLength = 16;
        public static int boardWidth = 7, boardHeight = 6;
        public static int minConsoleWidth = 48, minConsoleHeight = 14;
        static void Main(string[] args)
        {
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) => {e.Cancel = true;};
            Console.CursorVisible = false;
            Console.Clear();

            Player player1 = new Player("PlayerOne.......", ConsoleColor.Red);
            Player player2 = new Player("PlayerTwo.......", ConsoleColor.Yellow);
            GameScreen game = new GameScreen(player1, player2, boardWidth, boardHeight);
            game.Play();

            CloseProgram();
        }
        static void CloseProgram()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Environment.Exit(0);
        }
    }
}