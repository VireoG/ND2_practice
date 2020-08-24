using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework
{
    public class List_Of_Services : ILists<Service>
    {           
        public List_Of_Services()
        {
            servicelist = new List<Service>();
        }

        public List<Service> servicelist { get; set; }

        public List<Service> _AddList(Service service)
        {            
            servicelist.Add(service);
            
            return servicelist;
        }

        public void ShowList()
        {           
            foreach (var serv in servicelist)
            {
                Console.WriteLine($"ID: {serv.Index}\nDiscription: {serv.Description}\nCost: {serv.Cost}\n________\n");
            }
        }
    }
}
