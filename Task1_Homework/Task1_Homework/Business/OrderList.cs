using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Models
{
    public class OrderList
    {
        public List<Order> orders = new List<Order>();

        public Order[] GetOrder()
        {
            return orders.ToArray();
        }

    }
}
