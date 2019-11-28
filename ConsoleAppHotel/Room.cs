using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace ConsoleAppHotel
{
    class Room
    {
        private int id;
        private string name;
        private int adults;
        private int children;
        private List<Rate> rates;

        public int Id {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Adults
        {
            get { return this.adults; }
            set { this.adults = value; }
        }

        public int Children
        {
            get { return this.children; }
            set { this.children = value; }
        }

        public List<Rate> Rates
        {
            get { return this.rates; }
            set { this.rates = value; }
        }

        public Room(int id, string name, int adults, int children)
        {
            this.id = id;
            this.name = name;
            this.adults = adults;
            this.children = children;
            this.rates = new List<Rate>();
        }

        public Room(int id, string name, int adults, int children, List<Rate> ratesToAdd)
        {
            this.id = id;
            this.name = name;
            this.adults = adults;
            this.children = children;
            this.rates = new List<Rate>();
            foreach(Rate r in ratesToAdd)
            {
                this.AddRate(r);
            }
        }

        public void AddRate(Rate r)
        {
            this.rates.Add(r);
        }
        /// <summary>
        /// given a date intervale, calculate the price for rates with prices for that dates 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public decimal GetPriceForDays(DateTime startDate, DateTime endDate)
        {
            decimal price = 0;
            int numberOfDays = (endDate - startDate).Days;
            if(numberOfDays > 0)
            {
                if (this.rates.Count > 0)
                {
                    //for every day of interval given
                    foreach (DateTime dayFromInterval in EachDay(startDate, endDate))
                    {
                        //search in the list of rates the ones that have that day
                        var item = this.rates.FirstOrDefault(x => x.Day == dayFromInterval);
                        //if found, add to total price, the price of the rate
                        if (item != null)
                            price = price + item.Amount;
                    }
                }
                else
                {
                    Console.WriteLine("RoomType {0} has no rates", this.Name);
                }
            }
            else
            {
                Console.WriteLine("Interval {0} - {1} is not OK", startDate.ToString(), endDate.ToString());
            }
            
            return price;
        }
        public void Print()
        {
            Console.WriteLine("Properties of class Hotel are:");

            PropertyInfo[] propInfo;
            propInfo = this.GetType().GetProperties();

            foreach (PropertyInfo p in propInfo)
            {
                Console.WriteLine(" Property {0}", p.Name);
            }
        }
        /// <summary>
        /// used to walk thru a date interval
        /// </summary>
        /// <param name="from"></param>
        /// <param name="thru"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }

    
}
