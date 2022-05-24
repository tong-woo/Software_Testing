namespace Connect4
{
    public interface IConsoleIO {
        ConsoleColor ForegroundColor { get; set; }
        ConsoleColor BackgroundColor { get; set; }
        int WindowWidth { get; }
        int WindowHeight { get; }
        bool KeyAvailable {  get; }
        bool CursorVisible { set; }

        void Clear();
        void WriteLine(string message);
        void Write(string message);
        void Write(string format, object arg0);
        void Write(string format, object arg0, object arg1);
        string? ReadLine();
        ConsoleKeyInfo ReadKey(bool intercept);

        void SetCursorPosition(int x, int y);
    }

    public class ConsoleIO : IConsoleIO {
        public StringWriter? StringWriter;

        public ConsoleIO() {}
        public ConsoleIO(StringWriter sw) {
            StringWriter = sw;
        }

        public ConsoleColor ForegroundColor { 
            get { return StringWriter == null ? Console.ForegroundColor : ConsoleColor.White; } 
            set { if (StringWriter == null) Console.ForegroundColor = value; } 
        }
        public ConsoleColor BackgroundColor { 
            get { return StringWriter == null ? Console.BackgroundColor : ConsoleColor.Black; } 
            set { if (StringWriter == null) Console.BackgroundColor = value; } 
        }
        public int WindowWidth { get { return StringWriter == null ? Console.WindowWidth : 0; } }
        public int WindowHeight { get { return StringWriter == null ? Console.WindowHeight : 0; } }
        public bool KeyAvailable { get { return StringWriter == null ? Console.KeyAvailable : true; } }
        public bool CursorVisible { set { if (StringWriter == null) Console.CursorVisible = value; } }

        public void Clear() {
            if (StringWriter != null) StringWriter.Flush();
            else Console.Clear();
        }
        public void Write(string message) {
            if (StringWriter != null) StringWriter.Write(message);
            else Console.Write(message);
        }
        public void Write(string format, object arg0) {
            if (StringWriter != null) StringWriter.Write(format, arg0);
            else Console.Write(format, arg0);
        }
        public void Write(string format, object arg0, object arg1) {
            if (StringWriter != null) StringWriter.Write(format, arg0, arg1);
            else Console.Write(format, arg0, arg1);
        }
        public void WriteLine(string message) {
            if (StringWriter != null) StringWriter.WriteLine(message);
            else Console.WriteLine(message);
        }

        public string? ReadLine() {
            if (StringWriter == null) return Console.ReadLine();
            return "";
        }
        public ConsoleKeyInfo ReadKey(bool intercept) {
            if (StringWriter == null) return Console.ReadKey(intercept);
            return new();
        }

        public void SetCursorPosition(int x, int y) {
            if (StringWriter == null) Console.SetCursorPosition(x, y);
        }
    }
}