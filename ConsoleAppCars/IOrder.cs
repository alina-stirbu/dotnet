using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppCars
{
    interface IOrder
    {
        void Print();
        void CalculateTotalPrice();
        void CalculateDeliveryDate();
    }
}
