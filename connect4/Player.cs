namespace Connect4
{
    class Player
    {
        public string name; // Maybe make it a char[15/16] to enforce the length
        public ConsoleColor color;
        public bool isCurrentPlayer;

        public Player(string name, ConsoleColor color, bool current)
        {
            this.name = name;
            this.color = color;
            isCurrentPlayer = current;
        }


        public void Swap(){
            this.isCurrentPlayer = !this.isCurrentPlayer;
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