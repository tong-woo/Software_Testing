
using System;
using System.Timers;

namespace Connect4
{
    public class GameTimer
    {
        private System.Timers.Timer? _timer;
        private int sec;
        private int x, y;
        private readonly IConsoleIO ConsoleIO;
        public GameTimer(IConsoleIO consoleIO, int x, int y)
        {
            ConsoleIO = consoleIO;
            this.x = x;
            this.y = y;
        }

        public System.Timers.Timer? GetTimer
        {
            get { return _timer; }
        }

        public void Start(int seconds)
        {
            // Hook up the Elapsed event for the timer.
            this.sec = seconds;
            _timer = new();
            _timer.Interval = 1000;
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public void UpdatePosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw()
        {
            ConsoleIO.SetCursorPosition(x, y);
            ConsoleIO.Write("{0} ", sec);
        }

        public void Stop()
        {
            if (_timer is not null)
            {
                _timer.Close();
                _timer.Dispose();
                _timer = null;
            }
        }

        public bool Stopped {
            get => _timer == null;
        }


        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // TODO SafeDraw cancels execution of the function?
            sec--;
            if (sec == 0)
                Stop();
            Program.SafeDraw(ConsoleIO, false);
        }
    }
}




