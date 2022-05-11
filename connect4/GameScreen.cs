namespace Connect4
{
    class GameScreen
    {
        private Player player1;
        private Player player2;
        private Player currentTurn;
        private Board board;

        private GameTimer timer;

        private GameReferee referee;
        public GameScreen(Player player1, Player player2, int boardWidth, int boardHeight)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.currentTurn = player1;
            this.board = new Board(boardWidth, boardHeight);
            this.timer = new GameTimer(Console.WindowWidth / 2, 2);
            this.referee = new GameReferee(player1, player2, boardWidth, boardHeight);
        }

        
        public void Play()
        {
            Program.SetDrawScreen(Draw);

            timer.Start(30);
            while(true)
            {
                // Check if a key is pressed
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            board.SelectLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            board.SelectRight();
                            break;
                        case ConsoleKey.Enter:
                            // Do the move
                            (int x, int y) = board.Move(currentTurn);
                            timer.Stop();
                            Player? winner = referee.checkVictory(board, x, y);
                            
                            // Check if the game is over
                            if (winner is not null || board.isFull())
                            {
                                EndGame(winner);
                                return;
                            }

                            // Go to the next turn
                            timer.Start(30);

                            if (currentTurn == player1)
                                currentTurn = player2;
                            else
                                currentTurn = player1;

                            break;
                        case ConsoleKey.Escape:
                            Program.CloseProgram();
                            break;
                        default:
                            break;
                    }
                    Program.drawCurrentScreen();
                }
                // Check if the turn timer has ended
                if (timer.Stopped)
                {
                    if (currentTurn == player1)
                        EndGame(player2);
                    else
                        EndGame(player1);
                    return;
                }
                
                Thread.Sleep(50);
            }
        }

        private void Draw()
        {
            timer.UpdatePosition(Console.WindowWidth / 2, 2);
            timer.doDraw = true;
            Console.SetCursorPosition(1, 1);
            player1.Draw(currentTurn == player1);
            Console.SetCursorPosition(Console.WindowWidth - player2.name.Length - 5, 1);
            player2.Draw(currentTurn == player2);
            board.Draw(Console.WindowWidth / 2 - (board.columnAmount + 1), Console.WindowHeight / 2 - (board.rowAmount / 2 + 1));
            timer.Draw();
        }

        private void EndGame(Player? winner)
        {
            string text;
            if (winner is null)
                text = "The game ended in a draw!";
            else
                text = winner.name + " won! Congratulations!";
            timer.Stop();
            Draw();
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, 1);
            Console.Write(text);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 14, 2);
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}