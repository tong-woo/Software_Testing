namespace Connect4
{
    class GameReferee
    {
        private Player player1;
        private Player player2;

        public GameReferee(Player player1, Player player2, int boardWidth, int boardHeight) 
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        public Player? checkVictory(Board board, int xMove, int yMove)
        {
            Player? winner;
            // Check vertical
            winner = checkLine(board, xMove, yMove - 3, 0, 1);
            if (winner is not null)
                return winner;

            // Check horizontal
            winner = checkLine(board, xMove - 3, yMove, 1, 0);
            if (winner is not null)
                return winner;

            // Check diagonal bottom-left to top-right
            winner = checkLine(board, xMove - 3, yMove - 3, 1, 1);
            if (winner is not null)
                return winner;

            // Check diagonal bottom-right to top-left
            winner = checkLine(board, xMove + 3, yMove - 3, -1, 1);
            return winner;
        }

        private Player? checkLine(Board board, int xStart, int yStart, int xOffset, int yOffset)
        {
            int p1Score = 0;
            int p2Score = 0;

            for (int x = xStart, y = yStart, i = 0; i < 7; x += xOffset, y += yOffset, i++)
            {
                if (x < 0 || x >= Program.boardWidth || y < 0 || y >= Program.boardHeight)
                    continue;
                if (board.field[x][y] == null)
                {
                    p1Score = 0;
                    p2Score = 0;
                    continue;
                }
                else if (board.field[x][y] == player1)
                {
                    p1Score++;
                    p2Score = 0;
                }
                else if (board.field[x][y] == player2)
                {
                    p2Score++;
                    p1Score = 0;
                }
                if (p1Score == 4)
                    return player1;
                else if (p2Score == 4)
                    return player2;
            }
            return null;
        }
    }
}
