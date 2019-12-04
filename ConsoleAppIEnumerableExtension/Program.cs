using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppIEnumerableExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> intList = new List<int> { 1, 7, 9, 11 };

            int sumIntList = intList.Sum<int>();
            int maxIntList = intList.Max<int>();
            int minIntList = intList.Min();

            Console.WriteLine("Sum for intList is {0}",sumIntList);
            Console.WriteLine("Min for intList is {0}", minIntList);
            Console.WriteLine("Max for intList is {0}", maxIntList);
            Console.WriteLine("Product for intList is {0}", intList.Product());
            Console.WriteLine("Average for intList is {0}", intList.Average());

            //empty list of decimals
            List<decimal> decimalList = new List<decimal> {};
            Console.WriteLine("Sum for decimalList is {0}", decimalList.Sum());
            Console.WriteLine("Max for decimalList is {0}", decimalList.Max());
            Console.WriteLine("Average for decimalList is {0}", decimalList.Average());

            List<double> doubleList = new List<double> {11.3,4.5,8.0,12};

            double sumDoubleList = doubleList.Sum<double>();
            double maxDoubleList = doubleList.Max<double>();
            double minDoubleList = doubleList.Min();

            Console.WriteLine("Sum for doubleList is {0}", sumDoubleList);
            Console.WriteLine("Min for doubleList is {0}", maxDoubleList);
            Console.WriteLine("Max for doubleList is {0}", minDoubleList);
            Console.WriteLine("Product for doubleList is {0}", doubleList.Product());
            Console.WriteLine("Average for doubleList is {0}", doubleList.Average());

        }
    }
}
