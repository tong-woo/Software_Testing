
using System;
using System.Timers;

namespace Connect4
{
    class GameTimer
    {
        private System.Timers.Timer _timer;
        private int sec;
        public GameTimer(int Ms){
            sec = Ms/100 ;
            _timer = new System.Timers.Timer(Ms);
        }

        public System.Timers.Timer GetTimer{
            get{return _timer;}
        }

        public void countdown()
        {
            _timer.Elapsed += timer_Elapsed;
            _timer.Start(); Console.Read();
        }


        public void timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            sec--;
            Console.Clear();
            Console.WriteLine("************************************************");
            Console.WriteLine("           Please Move in 2 minutes             ");
            Console.WriteLine("");
            Console.WriteLine("         Time Remaining:  " + sec.ToString()      );
            Console.WriteLine("");
            Console.WriteLine("*************************************************");

            if (sec == 0)
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("************************************************");
                Console.WriteLine("               your opponent win!!!             ");
                Console.WriteLine("");
                Console.WriteLine("                      O V E R                   ");
                Console.WriteLine("************************************************");


                _timer.Close();
                _timer.Dispose();

            }

            GC.Collect();
        }
    }
}




