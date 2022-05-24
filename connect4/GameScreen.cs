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

        private string endText;
        private bool gameEnded;

        private readonly IConsoleIO ConsoleIO;


        public GameScreen(IConsoleIO consoleIO, Player player1, Player player2, int boardWidth, int boardHeight)
        {
            ConsoleIO = consoleIO;
            this.player1 = player1;
            this.player2 = player2;
            this.currentTurn = player1;
            this.board = new Board(ConsoleIO, boardWidth, boardHeight);
            this.timer = new GameTimer(ConsoleIO, ConsoleIO.WindowWidth / 2, 2);
            this.referee = new GameReferee(player1, player2, boardWidth, boardHeight);
            this.endText = "";
            this.gameEnded = false;
        }

        
        public void Play()
        {
            timer.Start(30);
            Program.SetDrawScreen(ConsoleIO, Draw);

            while(true)
            {
                Program.DrawOnResize(ConsoleIO);
                // Check if a key is pressed
                if (ConsoleIO.KeyAvailable)
                {
                    ConsoleKey key = ConsoleIO.ReadKey(true).Key;
                    if (gameEnded)
                        return;
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
                                EndGame(winner);
                            // Go to the next turn
                            else
                                timer.Start(30);

                            if (currentTurn == player1)
                                currentTurn = player2;
                            else
                                currentTurn = player1;

                            break;
                        case ConsoleKey.Escape:
                            Program.CloseProgram(ConsoleIO);
                            break;
                        default:
                            break;
                    }
                    Program.SafeDraw(ConsoleIO, false);
                }
                // Check if the turn timer has ended
                if (timer.Stopped)
                {
                    if (currentTurn == player1)
                        EndGame(player2);
                    else
                        EndGame(player1);
                }
                
                Thread.Sleep(10);
            }
        }

        private void Draw()
        {
            timer.UpdatePosition(ConsoleIO.WindowWidth / 2, 2);
            ConsoleIO.SetCursorPosition(1, 1);
            player1.Draw(currentTurn == player1);
            ConsoleIO.SetCursorPosition(ConsoleIO.WindowWidth - player2.name.Length - 5, 1);
            player2.Draw(currentTurn == player2);
            board.Draw(ConsoleIO.WindowWidth / 2 - (board.columnAmount + 1), ConsoleIO.WindowHeight / 2 - (board.rowAmount / 2 + 1), !gameEnded);
            if (gameEnded)
            {
                ConsoleIO.SetCursorPosition(ConsoleIO.WindowWidth / 2 - endText.Length / 2, 2);
                ConsoleIO.Write(endText);
                ConsoleIO.SetCursorPosition(ConsoleIO.WindowWidth / 2 - 14, 3);
                ConsoleIO.Write("Press any key to continue...");
            }
            else
                timer.Draw();
        }

        private void EndGame(Player? winner)
        {
            if (winner is null)
                endText = "The game ended in a draw!";
            else
                endText = winner.name + " won! Congratulations!";
            timer.Stop();
            gameEnded = true;
            while(ConsoleIO.KeyAvailable)
                ConsoleIO.ReadKey(true);
        }
    }
}