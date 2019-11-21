using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            String initial = "";
            String final = "";
            Console.WriteLine("Introduce a string:");
            initial = Console.ReadLine();
            if(initial.Length > 0)
            {
                foreach (char c in initial)
                {
                    if (final.IndexOf(c) == -1)
                        final = final + c;
                }
                Console.WriteLine($" String without duplicates is {final}");
            }
            else
            {
                Console.WriteLine("String has length = 0.");
            }
            
            Console.ReadKey();
        }
    }
}
