using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using ConsoleAppCars.Logger;

namespace ConsoleAppCars
{
    class Car:IVehicle
    {
        private string model;
        private int year;
        private Producer producer;
        private decimal price;

        public string Model
        {
            get { return this.model; }
            set { this.model = value; }
        }
        public int Year
        {
            get { return this.year; }
            set { this.year = value; }
        }
        public Producer Producer
        {
            get { return this.producer; }
            set { this.producer = value; }
        }
        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public Car(string model, Producer producer, int year)
        {
            this.model = model;
            this.Producer = producer;
            this.year = year;
        }

        public void Print()
        {
            LoggerClass logger = new LoggerClass(new ConsoleLog());
            logger.Log("In Class Car");
            logger.Log("  Properties of class Car are:");

            PropertyInfo[] propInfo;
            propInfo = this.GetType().GetProperties();

            foreach (PropertyInfo p in propInfo)
            {
                logger.Log($" Property {p.Name} Value {p.GetValue(this)}");
            }
        }
    }
}
