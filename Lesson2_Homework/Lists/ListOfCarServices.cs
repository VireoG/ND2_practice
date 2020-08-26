using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework
{
    public class ListOfCarServices : ILists<CarService>
    {
        public ListOfCarServices()
        {
            carServiceList = new List<CarService>();
        }

        public List<CarService> carServiceList { get; set; }

        public List<CarService> AddList(CarService CSM)
        {
            carServiceList.Add(CSM);

            return carServiceList;
        }

        public void ShowList()
        {
            foreach (var cserv in carServiceList)
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
