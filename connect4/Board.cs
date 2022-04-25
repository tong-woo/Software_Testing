namespace Connect4
{
    class Board
    {
        public int rowAmount, columnAmount;
        private Column[] field;
        private int selected;
        public Board(int width, int height)
        {
            columnAmount = width;
            rowAmount = height;
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

        public void Move(Player player)
        {
            field[selected].AddPiece(player);
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
                Console.SetCursorPosition(x, y + i + 2);
                DrawRow(i);
            }
            Console.SetCursorPosition(x, y + rowAmount + 2);
            DrawBottom();
        }

        private void DrawSelection()
        {
            Console.Write(" ");
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
            Console.Write("╔═");
            for (int i = 0; i < columnAmount; i++)
                Console.Write("══");
            Console.Write("╗");
        }

        private void DrawRow(int row)
        {
            Console.Write("║ ");
            for (int i = 0; i < columnAmount; i++)
            {
                Player? piece = field[i][row];
                if (piece is null)
                    Console.Write("○");
                else
                {
                    Console.ForegroundColor = piece.color;
                    Console.Write("●");
                    Console.ForegroundColor = Program.defaultColor;
                }
                Console.Write(" ");
            }
            Console.Write("║");
        }

        private void DrawBottom()
        {
            Console.Write("╚═");
            for (int i = 0; i < columnAmount; i++)
                Console.Write("══");
            Console.Write("╝");
        }
    }

    class Column
    {
        private Player?[] column;
        private int nextEmpty;
        public Column(int rows)
        {
            nextEmpty = rows - 1;
            column = new Player?[rows];
            for (int i = 0; i < rows; i++)
                column[i] = null;
        }

        public Player? this[int index]
        {
            get => column[index];
        }
        public bool isFull
        {
            get => nextEmpty < 0;
        }

        public void AddPiece(Player player)
        {
            if (isFull)
                throw new OverflowException("Column is full");
            column[nextEmpty] = player;
            nextEmpty--;
        }
    }
}