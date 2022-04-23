namespace Connect4
{
    class Board
    {
        private int rowAmount;
        private int columnAmount;
        private Column[] field;
        private int selected;
        private int turn;
        public Board(int width, int height)
        {
            columnAmount = width;
            rowAmount = height;
            turn = 1;
            field = new Column[columnAmount];
            for (int i = 0; i < columnAmount; i++)
                field[i] = new Column(rowAmount);
            selected = 0;
        }

        public void SelectLeft()
        {
            do
            {
                selected -= 1;
                if (selected < 0)
                    selected = columnAmount - 1;
            } while (field[selected].isFull);
        }
        public void SelectRight()
        {
            do
            {
                selected += 1;
                if (selected >= columnAmount)
                    selected = 0;
            } while (field[selected].isFull);
        }

        public bool isFull()
        {
            for (int i = 0; i < columnAmount; i++)
                if (!field[i].isFull)
                    return false;
            return true;
        }

        public void Move()
        {
            field[selected].AddPiece(turn);
            if (turn == 1)
                turn = 2;
            else
                turn = 1;
            for (int i = 0; i < columnAmount; i++)
                if (!field[i].isFull)
                {
                    selected = i;
                    break;
                }
        }

        public void Draw(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            DrawSelection();
            Console.SetCursorPosition(x, y + 1);
            DrawTop();
            for (int i = 0; i < rowAmount; i++)
            {
                Console.SetCursorPosition(x, y + i * 2 + 2);
                DrawRow(i);
                if (i != rowAmount - 1)
                {
                    Console.SetCursorPosition(x, y + i * 2 + 3);
                    DrawSeparator();
                }
            }
            Console.SetCursorPosition(x, y + rowAmount * 2 + 1);
            DrawBottom();
        }

        private void DrawSelection()
        {
            for (int i = 0; i < columnAmount; i++)
            {
                if (selected == i)
                    Console.Write(" v");
                else
                    Console.Write("  ");
            }
        }

        private void DrawTop()
        {
            Console.Write("╔");
            for (int i = 0; i < columnAmount; i++)
            {
                Console.Write("═");
                if (i != columnAmount - 1)
                    Console.Write("╦");
            }
            Console.Write("╗");
        }

        private void DrawRow(int row)
        {
            Console.Write("║");
            for (int i = 0; i < columnAmount; i++)
            {
                switch(field[i][row])
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("O");
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("O");
                        break;
                    default:
                        Console.Write(" ");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("║");
            }
        }

        private void DrawSeparator()
        {
            Console.Write("╠");
            for (int i = 0; i < columnAmount; i++)
            {
                Console.Write("═");
                if (i != columnAmount - 1)
                    Console.Write("╬");
            }
            Console.Write("╣");
        }

        private void DrawBottom()
        {
            Console.Write("╚");
            for (int i = 0; i < columnAmount; i++)
            {
                Console.Write("═");
                if (i != columnAmount - 1)
                    Console.Write("╩");
            }
            Console.Write("╝");
        }
    }

    class Column
    {
        private int[] column;
        private int nextEmpty;
        public Column(int rows)
        {
            nextEmpty = rows - 1;
            column = new int[rows];
            for (int i = 0; i < rows; i++)
                column[i] = 0;
        }

        public int this[int index]
        {
            get => column[index];
        }
        public bool isFull
        {
            get => nextEmpty < 0;
        }

        public void AddPiece(int player)
        {
            if (isFull)
                throw new OverflowException("Column is full");
            column[nextEmpty] = player;
            nextEmpty--;
        }
    }
}