using System;
using System.Collections.Generic;

namespace ConsoleAppHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Rate> rateList = new List<Rate>();
            Rate newRate = new Rate(12.5m, "EUR", new DateTime(2019, 12, 1));
            rateList.Add(newRate);
            rateList.Add(new Rate(12.8m, "EUR", new DateTime(2019, 12, 2)));
            rateList.Add(new Rate(14.5m, "EUR", new DateTime(2019, 12, 3)));
            rateList.Add(new Rate(18.5m, "EUR", new DateTime(2019, 12, 4)));
            rateList.Add(new Rate(16.5m, "EUR", new DateTime(2019, 12, 5)));
            rateList.Add(new Rate(17.5m, "EUR", new DateTime(2019, 12, 6)));
            rateList.Add(new Rate(15.5m, "EUR", new DateTime(2019, 12, 7)));

            Room room1 = new Room(1, "Room 1", 2, 0, rateList);

            List<Rate> rateList2 = new List<Rate>();
            rateList2.Add(new Rate(15.5m, "EUR", new DateTime(2019, 12, 1)));
            rateList2.Add(new Rate(18.8m, "EUR", new DateTime(2019, 12, 2)));
            rateList2.Add(new Rate(13.5m, "EUR", new DateTime(2019, 12, 3)));
            rateList2.Add(new Rate(17.5m, "EUR", new DateTime(2019, 12, 4)));
            rateList2.Add(new Rate(11.5m, "EUR", new DateTime(2019, 12, 5)));

            Room room2 = new Room(2, "Room 2", 2, 2, rateList2);
            Room room3 = new Room(3, "Room 3", 1, 0);

            List<Hotel> hotelList = new List<Hotel>();

            Hotel h1 = new Hotel("International","Iasi");
            h1.AddRoom(room1);
            h1.AddRoom(room2);
            h1.AddRoom(room3);
            hotelList.Add(h1);
            hotelList.Add(new Hotel("Ciric", "Iasi"));
            hotelList.Add(new Hotel("Moldova", "Iasi"));
            Console.WriteLine(" initial number of Hotels in List {0}", hotelList.Count);
            hotelList.RemoveAt(2);
            Console.WriteLine(" Final number of Hotels in List {0}", hotelList.Count);

            h1.Print();
            room1.Print();
            newRate.Print();

            decimal price = room1.GetPriceForDays(new DateTime(2019, 12, 2), new DateTime(2019, 12, 4));
            Console.WriteLine(" Price for period 2019.12.02-2019.12.04 for room {0} is {1:0.0000}", room1.Name, price.ToString());

            if(h1.FindRoomWithLowerPrice(15))
            {
                Console.WriteLine(" Hotel {0} has a Rate price < 15 for room {1}", h1.Name, h1.ResultRoom.Name);
            }
            else
            {
                Console.WriteLine(" Hotel {0} has no room with Rate price < 15", h1.Name);
            }

            decimal priceOfRooms = h1.GetPriceForNumberOfRooms(2, new DateTime(2019, 12, 2), new DateTime(2019, 12, 4));
            Console.WriteLine(" Price for period 2019.12.02-2019.12.04 for 2 rooms at Hotel {0} is {1:0.0000}", h1.Name, priceOfRooms.ToString());
        }
    }
}
