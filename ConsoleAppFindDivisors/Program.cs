using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppFindDivisors
{
    class Program
    {
        static void Main(string[] args)
        {
            var arraySize = 10000;
            
            var stopwatch = Stopwatch.StartNew();
            //no of cores
            int nr = Environment.ProcessorCount;
            Console.WriteLine($"Cores: {nr}");
            List<Divisors> divList = new List<Divisors>();
            List<Task> tasks = new List<Task>();
            //resulting dictionary after processing
            Dictionary<int, int> mergedDictionary = new Dictionary<int, int>();
            int batchSize = arraySize / nr;
            for (int i = 0; i < nr+1; i++)
            {
                int startIndex = i * batchSize;
                int batchSizeAdjusted = batchSize;
                if ((startIndex + batchSizeAdjusted) > arraySize)
                    batchSizeAdjusted = arraySize - startIndex;
                var divisor = new Divisors(startIndex, batchSizeAdjusted);
                divList.Add(divisor);
                var t = new Task(divisor.CalculateDivisors);
                tasks.Add(t);
                t.Start();
            }
            for (int i = 0; i < nr+1; i++)
            {
                tasks[i].Wait();
                mergedDictionary = mergedDictionary.Union(divList[i].DivisorsList).ToDictionary(d => d.Key, d => d.Value);
            }
            var maxDivisor = mergedDictionary.FirstOrDefault(p => p.Value == mergedDictionary.Max(p => p.Value));

            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine("Number wit max divisors is {0} and has {1} divisors: ", maxDivisor.Key, maxDivisor.Value);

        }
    }
}
