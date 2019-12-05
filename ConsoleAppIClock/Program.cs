using System;
using System.Globalization;

namespace ConsoleAppIClock
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock clock = new Clock();
            Console.WriteLine("From Clock Object, BussinesDate is {0}",clock.Today);

            // Create an array of all supported standard date and time format specifiers.
            string[] formats = {"d", "D", "f", "F", "g", "G", "m", "o", "r",
                          "s", "t", "T", "u", "U", "Y"};
            // Create an array of four cultures.                                 
            CultureInfo[] cultures = {CultureInfo.CreateSpecificCulture("de-DE"),
                                CultureInfo.CreateSpecificCulture("en-US"),
                                CultureInfo.CreateSpecificCulture("es-ES"),
                                CultureInfo.CreateSpecificCulture("fr-FR")};
           
            // Iterate each standard format specifier.
            foreach (string formatSpecifier in formats)
            {
                foreach (CultureInfo culture in cultures)
                    Console.WriteLine("{0} Format Specifier {1, 20} Culture {2, 40}",
                                      formatSpecifier, culture.Name,
                                      clock.Today.ToString(formatSpecifier, culture));
                Console.WriteLine();
            }
        }
    }
}
