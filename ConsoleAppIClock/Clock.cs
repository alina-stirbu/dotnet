using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppIClock
{
    public class Clock : IClock
    {
        public Clock()
        {
        }

        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        public BusinessDate Today => new BusinessDate(DateTime.Now);
    }
}
