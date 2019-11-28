using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppCars.Logger
{
    /// <summary>
    /// 
    /// class for logging on console
    /// </summary>
    public class ConsoleLog:ILogType
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
