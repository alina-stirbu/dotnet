using System;
using System.Reflection;
using System.Text;

namespace ConsoleAppHotel
{
    class Rate:IComparable<Rate>
    {
        private decimal amount;
        private string currency;
        private DateTime day;

        public decimal Amount {
            get { return this.amount; }
            set { this.amount = value;  } 
        }

        public string Currency {
            get { return this.currency; }
            set { this.currency = value; } 
        }

        public DateTime Day
        {
            get { return this.day; }
            set { this.day = value; }
        }

        public Rate(decimal amount, string currency, DateTime day)
        {
            this.amount = amount;
            this.currency = currency;
            this.day = day;
        }

        public int CompareTo(Rate r)
        {
            if(this.amount != r.amount)
            {
                if (this.amount < r.amount)
                    return -1;
                else
                    return 1;
            }
            return 0;
        }

        public void Print()
        {
            Console.WriteLine("Properties of class Rate are:");

            PropertyInfo[] propInfo;
            propInfo = this.GetType().GetProperties();

            foreach(PropertyInfo p in propInfo)
            {
                Console.WriteLine(" Property {0}", p.Name);
            }
        }
    }
}
