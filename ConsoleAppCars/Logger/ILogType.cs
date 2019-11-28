using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppCars.Logger
{
    /// <summary>
    /// interface for logging system
    /// </summary>
    public interface ILogType
    {
        void Log(string message);
    }
}
