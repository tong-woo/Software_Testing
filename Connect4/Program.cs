
using System.Text;

namespace Connect4
{
    public class Program
    {
        public static Action drawCurrentScreen = () => {};
        public static ConsoleColor defaultForegroundColor, defaultBackgroundColor;
        public static int nameLength = 16;
        public static int boardWidth = 7, boardHeight = 6;
        public static int minConsoleWidth = 48, minConsoleHeight = 14;
        private static Player? player1, player2;
        private static int currentWidth = 0, currentHeight = 0;
        static void Main(string[] args)
        {
            ConsoleIO ConsoleIO = new();
            // TODO
            Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) => {e.Cancel = true;};
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;
            defaultForegroundColor = ConsoleIO.ForegroundColor;
            defaultBackgroundColor = ConsoleIO.BackgroundColor;

            ToStartScreen(ConsoleIO);
        }
        static void ToStartScreen(IConsoleIO ConsoleIO)
        {
            player1 = null;
            player2 = null;
            StartScreen start = new StartScreen(ConsoleIO);
            (player1, player2) = start.Play();
            ToGameScreen(ConsoleIO);
        }
        static void ToGameScreen(IConsoleIO ConsoleIO)
        {
            GameScreen game = new GameScreen(ConsoleIO, player1!, player2!, boardWidth, boardHeight);
            game.Play();
            ToEndScreen(ConsoleIO);
        }
        static void ToEndScreen(IConsoleIO ConsoleIO)
        {
            EndScreen end = new EndScreen(ConsoleIO);
            int selection = end.Play();
            if (selection == 0)
                ToGameScreen(ConsoleIO);
            else if (selection == 1)
                ToStartScreen(ConsoleIO);
            else
                CloseProgram(ConsoleIO);
        }
        public static void CloseProgram(IConsoleIO ConsoleIO)
        {
            ConsoleIO.Clear();
            ConsoleIO.CursorVisible = true;
            Environment.Exit(0);
        }
        public static void SetDrawScreen(IConsoleIO ConsoleIO, Action draw)
        {
            drawCurrentScreen = draw;
            SafeDraw(ConsoleIO, true);
        }
        public static void SafeDraw(IConsoleIO ConsoleIO, bool clear)
        {
            if (clear)
                ConsoleIO.Clear();
            if (ConsoleIO.WindowWidth < Program.minConsoleWidth || ConsoleIO.WindowHeight < Program.minConsoleHeight)
            {
                ConsoleIO.SetCursorPosition(0, 0);
                ConsoleIO.Write("Console window should be at least {0}x{1}", Program.minConsoleWidth, Program.minConsoleHeight);
            }
            else
                drawCurrentScreen!();
        }
        public static void DrawOnResize(IConsoleIO ConsoleIO)
        {
            // Make sure the console fixes itself on resize
            if (currentWidth != ConsoleIO.WindowWidth || currentHeight != ConsoleIO.WindowHeight)
            {
                SafeDraw(ConsoleIO, true);
            }
            currentWidth = ConsoleIO.WindowWidth;
            currentHeight = ConsoleIO.WindowHeight;
        }
    }
}