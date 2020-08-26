using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson2_Homework.Adds
{
    public class AddService : IAddAndList<Service>
    {
        public Service Add(Service service)
        {
            Console.WriteLine("Input your service index:\n");
            string b = Console.ReadLine();
            service.Index = Convert.ToInt32(b);

            Console.WriteLine("\nEnter service description:\n");
            service.Description = Console.ReadLine();

            Console.WriteLine("\nEnter the cost of the service:\n");
            string c = Console.ReadLine();
            service.Cost = Convert.ToDecimal(c);

            return service;
        }

        public void AddInList(Service service)
        {
            var add = new ListOfServices();
            add.AddList(service);
        }
    }
}
