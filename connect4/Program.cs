
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
            int playerNum = 2;
            string[] name = new string[playerNum];
            string[] color = new string[playerNum];
            Dictionary<string, ConsoleColor> colorDict =  new Dictionary<string, ConsoleColor>();
            colorDict.Add("r", ConsoleColor.Red);
            colorDict.Add("y", ConsoleColor.Yellow);
            Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) => {e.Cancel = true;};
            Console.CursorVisible = false;
            defaultForegroundColor = Console.ForegroundColor;
            defaultBackgroundColor = Console.BackgroundColor;
            Console.Clear();
            for (int i = 0; i < playerNum; i++){
                Console.WriteLine("Enter Name: ");
                name[i] = Console.ReadLine();

                Console.WriteLine("pick color index: type r(red) or y(yellow)");
                color[i] = Console.ReadLine();
            }
            
            player1 = new Player(name[0], colorDict[color[0]]);
            player2 = new Player(name[1], colorDict[color[1]]);

            ToStartScreen();
        }
        static void ToStartScreen()
        {
            // TODO
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
                ToGameScreen(); // TODO
            else
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