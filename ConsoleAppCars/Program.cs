using ConsoleAppCars.Logger;
using System;

namespace ConsoleAppCars
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerClass logger = new LoggerClass(new ConsoleLog());

            logger.Log("Car App");

            Producer producerFord = new Producer("Ford");
            Producer producerSkoda = new Producer("Skoda");

            Store store1 = new Store("FordStore1", "Bucuresti, Sector 1", producerFord);
            Store store2 = new Store("SkodaStore", "Bucuresti Sector 2", producerSkoda);

            Car car1 = new Car("Focus", producerFord, 2015)
            {
                Price = 1500
            };

            Car car2 = new Car("Octavia", producerSkoda, 2017)
            {
                Price = 1400
            };

            Customer customer = new Customer("Alex", "Popescu", "0766543456", "pop@gmail.com");

            Order firstOrder = new Order("A1", new DateTime(2019, 11, 20))
            {
                Customer = customer,
                Car = car1,
                Quantity = 1,
                Store = store1
            };
            firstOrder.CalculateDeliveryDate();
            firstOrder.CalculateTotalPrice();
            firstOrder.Print();
            firstOrder.Car.Print();
            logger.Log($"For order {firstOrder.Number} delivery Day is {firstOrder.DeliveryDate}");

            Order secondOrder = new Order("A2", new DateTime(2019, 11, 25))
            {
                Customer = customer,
                Car = car2,
                Quantity = 1,
                Store = store2
            };
            secondOrder.CalculateDeliveryDate();
            secondOrder.CalculateTotalPrice();
            secondOrder.Print();
            secondOrder.Car.Print();
            logger.Log($"For order {secondOrder.Number} delivery Day is {secondOrder.DeliveryDate}");

            firstOrder.CancelOrder();

        }
    }
}
