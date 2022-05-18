namespace Connect4
{
    public class Player
    {
        public string name;
        public ConsoleColor color;

        public Player(string name, ConsoleColor color)
        {
            this.name = name;
            this.color = color;
        }

        public void Draw(bool isTurn)
        {
            if (isTurn)
                Console.Write("> ");
            else
                Console.Write("  ");

            Console.ForegroundColor = color;
            Console.Write(name);
            Console.ForegroundColor = Program.defaultForegroundColor;

            if (isTurn)
                Console.Write(" <");
            else
                Console.Write("  ");
        }
        
    }
}