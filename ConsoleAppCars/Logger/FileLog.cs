using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleAppCars.Logger
{
    /// <summary>
    /// Class for logging in file
    /// </summary>
    public class FileLog:ILogType
    {
        private StreamWriter file;

        public FileLog(string filePath)
        {
            this.file = new StreamWriter(filePath);
        }
        public void Log(string message)
        {
            this.file.WriteLine(message);
        }
    }
}
