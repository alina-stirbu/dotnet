using System;

namespace ConsoleAppTimer
{
    public delegate void TimerDelegate(string messageText);
    class Program
    {
        static void Main(string[] args)
        {
            TimerDelegate d = new TimerDelegate(DisplayMessage);
            Timer1 timer = new Timer1(100);
            timer.MessageArrived += d;
        }
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
