namespace Connect4
{
    class Player
    {
        public string name; // Maybe make it a char[15/16] to enforce the length
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
            Console.ForegroundColor = Program.defaultColor;

            if (isTurn)
                Console.Write(" <");
            else
                Console.Write("  ");
        }
    }
}