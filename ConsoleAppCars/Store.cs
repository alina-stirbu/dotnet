using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using ConsoleAppCars.Logger;

namespace ConsoleAppCars
{
    class Store:IStore
    {
        private string name;
        private string location;
        private Producer producer;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        public Producer Producer
        {
            get { return this.producer; }
            set { this.producer = value; }
        }

        public Store(string name, string location, Producer producer)
        {
            this.name = name;
            this.location = location;
            this.producer = producer;
        }
        public void Print()
        {
            LoggerClass logger = new LoggerClass(new ConsoleLog());
            logger.Log("In Class Store");
            logger.Log("Properties of class Store are:");

            PropertyInfo[] propInfo;
            propInfo = this.GetType().GetProperties();

            foreach (PropertyInfo p in propInfo)
            {
                logger.Log($" Property {p.Name} Value {p.GetValue(this)}");
            }
        }
    }
}
