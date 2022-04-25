
using System;
using System.Collections.Generic;

namespace Connect4
{
    class Program
    {
        
        public static ConsoleColor defaultColor = ConsoleColor.White;
        public static int nameLength = 16;
        public static int boardWidth = 7, boardHeight = 6;
        public static int minConsoleWidth = 48, minConsoleHeight = 14;
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
            Console.Clear();
            for (int i = 0; i < playerNum; i++){
                Console.WriteLine("Enter Name: ");
                name[i] = Console.ReadLine();

                Console.WriteLine("pick color index: type r(red) or y(yellow)");
                color[i] = Console.ReadLine();
            }
            GameTimer timer = new GameTimer(12000);

            // Player player1 = new Player("PlayerOne.......", ConsoleColor.Red, true);
            // Player player2 = new Player("PlayerTwo.......", ConsoleColor.Yellow, false);
            
            Player player1 = new Player(name[0], colorDict[color[0]], true);
            Player player2 = new Player(name[1], colorDict[color[1]], false);


            GameScreen game = new GameScreen(player1, player2, boardWidth, boardHeight);
            game.Play();

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