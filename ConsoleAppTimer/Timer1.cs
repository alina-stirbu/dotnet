using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using static ConsoleAppTimer.Program;

namespace ConsoleAppTimer
{
    public class Timer1
    {
        public event TimerDelegate MessageArrived;
        private Timer CurrentTimer;

        public Timer1(int t)
        {
            CurrentTimer = new Timer(t);
            CurrentTimer.Elapsed += new ElapsedEventHandler(CustomMethod);
            CurrentTimer.Start();
        }

        private void CustomMethod(object source, ElapsedEventArgs e)
        {
            if(MessageArrived == null)
            {
                MessageArrived("Event from Timer1");
            }
        }
    }
}
