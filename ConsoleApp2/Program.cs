using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arrayValues = { 1, 2, 43, 1, 7, 1, 1 };
            Dictionary<int, int> dict = new Dictionary<int, int>();

            Console.WriteLine("array values are:");
            foreach (var item in arrayValues)
            {
                Console.WriteLine(item.ToString());
            }

            for (int i = 0; i < arrayValues.Length; i++)
            {
                if (dict.ContainsKey(arrayValues[i]))
                {
                    int oldNumberOfOccurencies = dict[arrayValues[i]];
                    dict[arrayValues[i]] = oldNumberOfOccurencies + 1;
                }
                else
                {
                    dict.Add(arrayValues[i], 1);
                }
            }

            foreach (KeyValuePair<int, int> k in dict)
            {
                //Console.WriteLine(k.Key + " " + k.Value);
                if (k.Value > (arrayValues.Length / 2))
                {
                    Console.WriteLine(k.Key + " is the majority number");
                    break;
                }
            }
            
        }
    }
}
