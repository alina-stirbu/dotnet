using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppCars.Logger
{ 
    public class LoggerClass
    {
        private ILogType logType;

        public LoggerClass(ILogType log)
        {
            this.logType = log;
        }

        public void Log(string message)
        {
            this.logType.Log(message);
        }
    }
}
