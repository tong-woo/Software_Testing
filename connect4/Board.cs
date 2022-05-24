namespace Connect4
{
    public class Board
    {
        public int rowAmount, columnAmount;
        public Column[] field;
        private int selection;
        private IConsoleIO ConsoleIO;

        public Board(IConsoleIO consoleIO, int width, int height)
        {
            ConsoleIO = consoleIO;
            columnAmount = width;
            rowAmount = height;
            field = new Column[columnAmount];
            for (int i = 0; i < columnAmount; i++)
                field[i] = new Column(rowAmount);
            selection = 0;
        }

        public void SelectLeft()
        {
            do
            {
                selection -= 1;
                if (selection < 0)
                    selection = columnAmount - 1;
            } while (field[selection].isFull);
        }
        public void SelectRight()
        {
            do
            {
                selection += 1;
                if (selection >= columnAmount)
                    selection = 0;
            } while (field[selection].isFull);
        }

        public bool isFull()
        {
            for (int i = 0; i < columnAmount; i++)
                if (!field[i].isFull)
                    return false;
            return true;
        }

        public (int, int) Move(Player player)
        {
            int x, y;
            x = selection;
            y = field[selection].AddPiece(player);
            for (int i = 0; i < columnAmount; i++)
                if (!field[i].isFull)
                {
                    selection = i;
                    break;
                }
            return (x, y);
        }

        public void Draw(int x, int y, bool drawSelection)
        {
            if (drawSelection)
            {
                ConsoleIO.SetCursorPosition(x, y);
                DrawSelection();
            }
            ConsoleIO.SetCursorPosition(x, y + 1);
            DrawTop();
            for (int i = 0; i < rowAmount; i++)
            {
                ConsoleIO.SetCursorPosition(x, y + i + 2);
                DrawRow(i);
            }
            ConsoleIO.SetCursorPosition(x, y + rowAmount + 2);
            DrawBottom();
        }

        private void DrawSelection()
        {
            ConsoleIO.Write(" ");
            for (int i = 0; i < columnAmount; i++)
            {
                if (selection == i)
                    ConsoleIO.Write(" v");
                else
                    ConsoleIO.Write("  ");
            }
        }

        private void DrawTop()
        {
            ConsoleIO.Write("╔═");
            for (int i = 0; i < columnAmount; i++)
                ConsoleIO.Write("══");
            ConsoleIO.Write("╗");
        }

        private void DrawRow(int row)
        {
            ConsoleIO.Write("║ ");
            for (int i = 0; i < columnAmount; i++)
            {
                Player? piece = field[i][row];
                if (piece is null)
                    ConsoleIO.Write("○");
                else
                {
                    ConsoleIO.ForegroundColor = piece.color;
                    ConsoleIO.Write("●");
                    ConsoleIO.ForegroundColor = Program.defaultForegroundColor;
                }
                ConsoleIO.Write(" ");
            }
            ConsoleIO.Write("║");
        }

        private void DrawBottom()
        {
            ConsoleIO.Write("╚═");
            for (int i = 0; i < columnAmount; i++) 
                ConsoleIO.Write("══");
            ConsoleIO.Write("╝");
        }
    }

    public class Column
    {
        private Player?[] column;
        private int nextEmpty;
        public Column(int rows)
        {
            nextEmpty = rows - 1;
            column = new Player?[rows];
        }

        public Player? this[int index]
        {
            get => column[index];
        }
        public bool isFull
        {
            get => nextEmpty < 0;
        }

        public int AddPiece(Player player)
        {
            if (isFull)
                throw new OverflowException("Column is full");
            column[nextEmpty] = player;
            nextEmpty--;
            return nextEmpty + 1;
        }
    }
}