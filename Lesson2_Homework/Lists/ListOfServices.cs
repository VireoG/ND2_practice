using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework
{
    public class ListOfServices : ILists<Service>
    {           
        public ListOfServices()
        {
            serviceList = new List<Service>();
        }

        public List<Service> serviceList { get; set; }

        public List<Service> AddList(Service service)
        {            
            serviceList.Add(service);
            
            return serviceList;
        }

        public void ShowList()
        {           
            foreach (var serv in serviceList)
            {
                Console.WriteLine($"ID: {serv.Index}\nDiscription: {serv.Description}\nCost: {serv.Cost}\n________\n");
            }
        }
    }
}
