namespace Connect4
{
    public class Player
    {
        public string name;
        public ConsoleColor color;
        private readonly IConsoleIO ConsoleIO;

        public Player(IConsoleIO consoleIO, string name, ConsoleColor color)
        {
            ConsoleIO = consoleIO;
            this.name = name;
            this.color = color;
        }

        public void Draw(bool isTurn)
        {
            if (isTurn)
                ConsoleIO.Write("> ");
            else
                ConsoleIO.Write("  ");

            ConsoleIO.ForegroundColor = color;
            ConsoleIO.Write(name);
            ConsoleIO.ForegroundColor = Program.defaultForegroundColor;

            if (isTurn)
                ConsoleIO.Write(" <");
            else
                ConsoleIO.Write("  ");
        }
        
    }
}