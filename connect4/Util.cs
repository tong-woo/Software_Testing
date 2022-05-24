namespace Connect4
{
    public class ScreenOption
    {
        public string text;
        public ConsoleColor color;
        private readonly IConsoleIO ConsoleIO;

        public ScreenOption(IConsoleIO consoleIO, string text) {
            ConsoleIO = consoleIO;
            this.text = text;
            this.color = Program.defaultForegroundColor;
        }
        public ScreenOption(IConsoleIO consoleIO, string text, ConsoleColor color)
        {
            ConsoleIO = consoleIO;
            this.text = text;
            this.color = color;
        }
        public void Draw()
        {
            ConsoleIO.ForegroundColor = this.color;
            ConsoleIO.Write(text);
            ConsoleIO.ForegroundColor = Program.defaultForegroundColor;
        }
    }
}