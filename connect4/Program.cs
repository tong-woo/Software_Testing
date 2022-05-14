
using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4
{
    class Program
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
            Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) => {e.Cancel = true;};
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;
            defaultForegroundColor = Console.ForegroundColor;
            defaultBackgroundColor = Console.BackgroundColor;

            ToStartScreen();
        }
        static void ToStartScreen()
        {
            player1 = null;
            player2 = null;
            StartScreen start = new StartScreen();
            (player1, player2) = start.Play();
            ToGameScreen();
        }
        static void ToGameScreen()
        {
            GameScreen game = new GameScreen(player1!, player2!, boardWidth, boardHeight);
            game.Play();
            ToEndScreen();
        }
        static void ToEndScreen()
        {
            EndScreen end = new EndScreen();
            int selection = end.Play();
            if (selection == 0)
                ToGameScreen();
            else if (selection == 1)
                ToStartScreen();
            else
                CloseProgram();
        }
        public static void CloseProgram()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Environment.Exit(0);
        }
        public static void SetDrawScreen(Action draw)
        {
            drawCurrentScreen = draw;
            SafeDraw(true);
        }
        public static void SafeDraw(bool clear)
        {
            if (clear)
                Console.Clear();
            if (Console.WindowWidth < Program.minConsoleWidth || Console.WindowHeight < Program.minConsoleHeight)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("Console window should be at least {0}x{1}", Program.minConsoleWidth, Program.minConsoleHeight);
            }
            else
                drawCurrentScreen!();
        }
        public static void DrawOnResize()
        {
            // Make sure the console fixes itself on resize
            if (currentWidth != Console.WindowWidth || currentHeight != Console.WindowHeight)
            {
                SafeDraw(true);
            }
            currentWidth = Console.WindowWidth;
            currentHeight = Console.WindowHeight;
        }
    }
}