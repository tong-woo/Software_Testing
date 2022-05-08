
using System;
using System.Collections.Generic;

namespace Connect4
{
    class Program
    {
        
        public static ConsoleColor defaultForegroundColor, defaultBackgroundColor;
        public static int nameLength = 16;
        public static int boardWidth = 7, boardHeight = 6;
        public static int minConsoleWidth = 48, minConsoleHeight = 14;
        private static Player? player1, player2;
        static void Main(string[] args)
        {
            Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) => {e.Cancel = true;};
            Console.CursorVisible = false;
            defaultForegroundColor = Console.ForegroundColor;
            defaultBackgroundColor = Console.BackgroundColor;

            ToStartScreen();
        }
        static void ToStartScreen()
        {
            player1 = null;
            player2 = null;
            Console.Clear();
            StartScreen start = new StartScreen();
            (player1, player2) = start.Play();
            ToGameScreen();
        }
        static void ToGameScreen()
        {
            Console.Clear();
            GameScreen game = new GameScreen(player1!, player2!, boardWidth, boardHeight);
            game.Play();
            ToEndScreen();
        }
        static void ToEndScreen()
        {
            Console.Clear();
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
    }
}