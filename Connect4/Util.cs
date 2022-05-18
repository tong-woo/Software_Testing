namespace Connect4
{
    class ScreenOption
    {
        public string text;
        public ConsoleColor color;
        public ScreenOption(string text)
        {
            this.text = text;
            this.color = Program.defaultForegroundColor;
        }
        public ScreenOption(string text, ConsoleColor color)
        {
            this.text = text;
            this.color = color;
        }
        public void Draw()
        {
            Console.ForegroundColor = this.color;
            Console.Write(text);
            Console.ForegroundColor = Program.defaultForegroundColor;
        }
    }
}