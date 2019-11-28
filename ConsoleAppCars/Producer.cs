using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using ConsoleAppCars.Logger;

namespace ConsoleAppCars
{
    class Producer:IProducer
    {
        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public Producer(string name)
        {
            this.name = name;
        }
        public void Print()
        {
            LoggerClass logger = new LoggerClass(new ConsoleLog());
            logger.Log("In Class Producer");
            logger.Log("Properties of class Producer are:");

            PropertyInfo[] propInfo;
            propInfo = this.GetType().GetProperties();

            foreach (PropertyInfo p in propInfo)
            {
                logger.Log($" Property {p.Name} Value {p.GetValue(this)}");
            }
        }
    }
}
