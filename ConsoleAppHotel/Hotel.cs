using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppHotel
{
    class Hotel
    {
        private string name;
        private string city;
        private List<Room> roomList;
        //used to store the result of search room 
        private Room resultRoom;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string City
        {
            get { return this.city; }
            set { this.city = value; }
        }

        public List<Room> RoomList
        {
            get { return this.roomList; }
            set { this.roomList = value; }
        }

        public Room ResultRoom { get; private set; }

        public Hotel(string name, string city)
        {
            this.name = name;
            this.city = city;
            this.roomList = new List<Room>();
        }
        public Hotel(string name, string city, List<Room> rooms)
        {
            this.name = name;
            this.city = city;
            this.roomList = new List<Room>();
            foreach (Room r in rooms)
            {
                this.AddRoom(r);
            }
        }
        /// <summary>
        /// get price for a number of rooms of hotel and a period of time
        /// </summary>
        /// <param name="numberOfRooms"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public decimal GetPriceForNumberOfRooms(int numberOfRooms, DateTime startDate, DateTime endDate)
        {
            decimal price = 0;
            if(this.roomList.Count >= numberOfRooms)
            {
                for (int i = 0; i < this.roomList.Count; i++)
                {
                    if (i > numberOfRooms-1)
                    {
                        break;
                    }
                    price += this.roomList[i].GetPriceForDays(startDate, endDate);
                }
            }
            else
            {
                Console.WriteLine("Hotel {0} doesn't have {1} rooms available",this.name,numberOfRooms);
            }
            
            return price;

        }
        /// <summary>
        /// check in the list of rooms the first one that has Rates with Amount < price
        /// 
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public bool FindRoomWithLowerPrice(decimal price)
        {
            if(this.roomList.Count > 0)
            {
                foreach (Room r in this.roomList)
                {
                    r.Rates.Sort();
                    if (r.Rates[0].Amount <= price)
                    {
                        ResultRoom = r;
                        return true;
                    } 
                }
            }
            return false;

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

        public void AddRoom(Room r)
        {
            this.roomList.Add(r);
        }
    }
}
