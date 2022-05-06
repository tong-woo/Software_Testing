    namespace Connect4
    {
        class GameReferee
        {
            public void player1Win()
            {
                Console.SetCursorPosition(10, 10);
                Console.Write("Congratulations on player 1 winning!");
                
                //Console.ReadKey();
            }

            public void player2Win()
            {
                Console.SetCursorPosition(10, 10);
                Console.WriteLine("Congratulations on player 2 winning!");
                
                //Console.ReadKey();
            }

            public void endinDraw()
            {
                Console.WriteLine("Draw!");
                Console.ReadKey();
            }


            private Player player1;
            private Player player2;
            private Board board;

            public GameReferee(Player player1, Player player2, int boardWidth, int boardHeight) {
                this.player1 = player1;
                this.player2 = player2;
                this.board = new Board(boardWidth, boardHeight);
            }

            public int checkVictory(Board latestboard)
            {
                //update board
                board = latestboard;

                int flag1 = 0;
                int flag2 = 0;
                Player? piece;

                //check vertical
                for (int c = 0; c < board.columnAmount; c++) {
                    flag1 = 0;
                    flag2 = 0;
                    for (int r = 0; r < board.rowAmount; r++) {
                        piece = board.field[c][r];
                        if (piece is null)
                        {
                            continue;
                        }
                        else if (piece == player1)
                        {
                            flag1++;
                            flag2 = 0;
                        }
                        else if (piece == player2) {
                            flag2++;
                            flag1 = 0;
                        }

                        if (flag1 == 4) {
                            //player1 wins
                            player1Win();
                            return 1;
                        }
                        if (flag2 == 4) {
                            //player2 wins
                            player2Win();
                            return 2;
                        }
                    }
                }

                //check horizonal
                for (int r = 0; r < board.rowAmount; r++)
                {
                    flag1 = 0;
                    flag2 = 0;
                    for (int c = 0; c < board.columnAmount; c++)
                    {
                        piece = board.field[c][r];

                        if (piece is null)
                        {
                            continue;
                        }
                        else if (piece == player1)
                        {
                            flag1++;
                            flag2 = 0;
                        }
                        else if (piece == player2)
                        {
                            flag2++;
                            flag1 = 0;
                        }

                        if (flag1 == 4)
                        {
                            //player1 wins
                            player1Win();
                            return 1;
                        }
                        if (flag2 == 4)
                        {
                            //player2 wins
                            player1Win();
                            return 2;
                        }
                    }
                }

                //check diagonal, from left-top to right-bottom
                for (int startc = -2; startc < 4; startc++)
                {
                    flag1 = 0;
                    flag2 = 0;
                for (int c = startc, r = 0; r < board.rowAmount; c++, r++)
                    {
                        //flag1 = 0;
                        //flag2 = 0;

                        if (c < 0 || c > 6) continue;

                        piece = board.field[c][r];

                        if (piece is null)
                        {
                            continue;
                        }
                        else if (piece == player1)
                        {
                            flag1++;
                            flag2 = 0;
                        }
                        else if (piece == player2)
                        {
                            flag2++;
                            flag1 = 0;
                        }

                        if (flag1 == 4)
                        {
                            //player1 wins
                            player1Win();
                            return 1;
                        }
                        if (flag2 == 4)
                        {
                            //player2 wins
                            player1Win();
                            return 2;
                        }
                    }
                }

                //check diagonal, from right-top to left-bottom
                for (int startc = 3; startc < 9; startc++)
                {
                flag1 = 0;
                flag2 = 0;
                for (int c = startc, r = 0; r < board.rowAmount; c--, r++)
                    {
                        
                        if (c < 0 || c > 6) continue;

                        piece = board.field[c][r];

                        if (piece is null)
                        {
                            continue;
                        }
                        else if (piece == player1)
                        {
                            flag1++;
                            flag2 = 0;
                        }
                        else if (piece == player2)
                        {
                            flag2++;
                            flag1 = 0;
                        }

                        if (flag1 == 4)
                        {
                            //player1 wins
                            player1Win();
                            return 1;
                        }
                        if (flag2 == 4)
                        {
                            //player2 wins
                            player1Win();
                            return 2;
                        }
                    }
                }

                //no winner

                //draw
                if (board.isFull()) return 3;

                return 0;
            }
        }

    }

  
            
        
    

