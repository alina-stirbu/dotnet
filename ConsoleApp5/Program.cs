﻿using System;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Introduce list content!");
            string text = Console.ReadLine();
            if (text.Length > 0)
            {
                LinkedList list = new LinkedList();
                foreach (char c in text)
                {
                    list.AddNode(new Node(c));
                }
                Console.WriteLine("Before removing duplicates:");
                list.DisplayList();
                list.RemoveDuplicatesFromList();
                Console.WriteLine("\nAfter removing duplicates:");
                list.DisplayList();
            }
            else
            {
                Console.WriteLine("Introduce a text with length > 0 ");
            }
        }
    }
}
