using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string initial;
            int[] occurences = new int[char.MaxValue];
            Console.WriteLine("Introduce a string:");
            initial = Console.ReadLine();
            if(initial.Length > 0)
            {
                foreach (char c in initial)
                {
                    occurences[(int)c]++;
                }
                for(int i = 0; i < occurences.Length-1; i++)
                {
                    if(occurences[i]>=1)
                    {
                        Console.WriteLine((char)i + " occurs for " + occurences[i] + " times");
                    }
                }
            }
            else
            {
                Console.WriteLine("String should have length > 0");
            }

        }
    }
}
