using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppFindDivisors
{
    /// <summary>
    /// class used to store the list of numbers and their divisors
    /// </summary>
    class Divisors
    {
        private readonly int nrOfElementsToProcess;
        private readonly int startIndex;
        public Dictionary<int,int> DivisorsList { get; private set; }

        public Divisors(int startIndex, int nrOfElementsToProcess)
        {
            DivisorsList = new Dictionary<int, int>();
            this.startIndex = startIndex;
            this.nrOfElementsToProcess = nrOfElementsToProcess;
            for(int i = startIndex; i< nrOfElementsToProcess; i++)
            {
                DivisorsList.Add(i,0);
            }
        }
        /// <summary>
        /// method used to calc divisors for an interval
        /// </summary>
        public void CalculateDivisors()
        {
            var to = this.startIndex + this.nrOfElementsToProcess;

            for (var i = this.startIndex; i < to; i++)
            {
                DivisorsList[i] = this.ProcessDivisor(i);
            }
            Console.WriteLine("In task {0} - {1}", this.startIndex, to);
        }

        /// <summary>
        /// method used to calc divisor for a specific number
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int ProcessDivisor(int i)
        {
            int number = 0;
            for (int k = 1; k <= Math.Sqrt(i); k++)
            {
                if(i%k == 0)
                {
                    if (i / k == k)
                        number++;
                    else
                        number += 2;
                }
            }
            return number;
        }
    }
}
