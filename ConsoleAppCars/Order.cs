using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ConsoleAppCars.Logger;

namespace ConsoleAppCars
{
    class Order:IOrder
    {
        private string number;
        private DateTime orderedDate;
        private DateTime deliveryDate;
        private Store store;
        private Customer customer;
        private Car car;
        private int quantity;
        private decimal totalPrice;
        private int status;

        public string Number
        {
            get { return this.number; }
            set { this.number = value; }
        }
        public DateTime OrderedDate
        {
            get { return this.orderedDate; }
            set { this.orderedDate = value; }
        }
        public DateTime DeliveryDate
        {
            get { return this.deliveryDate; }
            set { this.deliveryDate = value; }
        }
        public Store Store
        {
            get { return this.store; }
            set { this.store = value; }
        }
        public Customer Customer
        {
            get { return this.customer; }
            set { this.customer = value; }
        }

        public Car Car
        {
            get { return this.car; }
            set { this.car = value; }
        }
        public int Quantity
        {
            get { return this.quantity; }
            set { this.quantity = value; }
        }
        public decimal TotalPrice
        {
            get { return this.totalPrice; }
            set { this.totalPrice = value; }
        }
        public int Status
        {
            get { return this.status; }
        }

        public Order(string number, DateTime date)
        {
            this.number = number;
            this.orderedDate = date;
            this.status = 1;
        }
        public void CancelOrder()
        {
            this.status = 0;
        }

        public void CalculateTotalPrice()
        {
            this.totalPrice = this.Car.Price * this.quantity;
        }
        /// <summary>
        /// 
        /// calculate the value of orderedDate depending on store name
        /// </summary>
        public void CalculateDeliveryDate()
        {
            //default value
            this.deliveryDate = this.orderedDate.AddDays(84);
            if (this.store.Name.Contains("Ford"))
                this.deliveryDate = this.orderedDate.AddDays(28);
            if (this.store.Name.Contains("Skoda"))
                this.deliveryDate = this.orderedDate.AddDays(21);
        }
        public void Print()
        {
            LoggerClass logger = new LoggerClass(new ConsoleLog());
            logger.Log("In Class Order");
            logger.Log("Properties of class Order are:");

            PropertyInfo[] propInfo;
            propInfo = this.GetType().GetProperties();

            foreach (PropertyInfo p in propInfo)
            {
                logger.Log($" Property {p.Name} Value {p.GetValue(this)}");
            }
        }
    }
}
