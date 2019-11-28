using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using ConsoleAppCars.Logger;

namespace ConsoleAppCars
{
    class Customer:IPerson
    {
        private string name;
        private string surname;
        private string phone;
        private string email;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Surname
        {
            get { return this.surname; }
            set { this.surname = value; }
        }
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        public Customer(string name, string surname, string phone, string email)
        {
            this.name = name;
            this.surname = surname;
            this.phone = phone;
            this.email = email;
        }

        public void Print()
        {
            LoggerClass logger = new LoggerClass(new ConsoleLog());
            logger.Log("In Class Customer");
            logger.Log("Properties of class Customer are:");

            PropertyInfo[] propInfo;
            propInfo = this.GetType().GetProperties();

            foreach (PropertyInfo p in propInfo)
            {
                logger.Log($" Property {p.Name} Value {p.GetValue(this)}");
            }
        }
    }
}
