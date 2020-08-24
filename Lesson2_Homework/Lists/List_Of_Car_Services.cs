using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework
{
    public class List_Of_Car_Services : ILists<Car_Service>
    {
        public List_Of_Car_Services()
        {
            car_servicelist = new List<Car_Service>();
        }

        public List<Car_Service> car_servicelist { get; set; }

        public List<Car_Service> _AddList(Car_Service CSM)
        {
            car_servicelist.Add(CSM);

            return car_servicelist;
        }

        public void ShowList()
        {
            foreach (var cserv in car_servicelist)
            {
                Console.WriteLine($"Name of Car Service: {cserv.ServiceName}\nService List:");

                foreach ( var cser in cserv.Services)
                {                    
                    Console.WriteLine($"\nID: {cser.Index}\nDiscription: {cser.Description}\nCost: {cser.Cost}\n________\n");
                }
            }
        }
    }
}
