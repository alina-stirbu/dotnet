using System;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Introduce list content!");
            string text = Console.ReadLine();
            if (text.Length > 0)
            {
                //last occurence of space
                int lastSpace = text.LastIndexOf(' ');
                if (lastSpace > -1)
                {
                    Console.WriteLine(text.Length - (lastSpace+1));
                }
                else
                {
                    Console.WriteLine('0');
                }
            }
            else
            {
                Console.WriteLine("Introduce a text with length > 0 ");
            }
        }
    }
}
