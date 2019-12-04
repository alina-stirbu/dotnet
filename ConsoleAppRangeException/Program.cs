using System;
using System.Collections.Generic;

namespace ConsoleAppRangeException
{
    class Program
    {
        public static readonly int startInt = 0;
        public static readonly int stopInt = 100;
        public static readonly DateTime startDate = new DateTime(1980, 1, 1);
        public static readonly DateTime stopDate = new DateTime(2013, 12, 31);

        static void Main(string[] args)
        {
            List<int> intRange = new List<int> { 1, 2, 100, 5, -1};
            List<DateTime> dateRange = new List<DateTime> { new DateTime(1980, 1, 2), new DateTime(1999, 03, 05), new DateTime(2020, 4, 5) };
            
            try
            {
                foreach (var elem in intRange)
                {
                    if (!IsInIntRange(elem))
                    {
                        throw new InvalidRangeException<int>($"Elem is not in range: {elem}",startInt, stopInt);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //used for date time
            try
            {
                foreach (var elem in dateRange)
                {
                    if (!IsInDateRange(elem))
                    {
                        throw new InvalidRangeException<DateTime>($"Elem is not in range: {elem}", startDate, stopDate);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static bool IsInIntRange(int element)
        {
            if (element >= startInt && element <= stopInt)
                return true;
            return false;
        }

        public static bool IsInDateRange(DateTime element)
        {
            if (element >= startDate && element <= stopDate)
                return true;
            return false;
        }
    }
}
