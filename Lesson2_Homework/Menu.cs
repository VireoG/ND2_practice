using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework
{
    public class Menu
    {
        public void _Menu()
        {

            Console.WriteLine("Make an order -> enter 1;\nCreate car service -> enter 2;\n" +
                    "Add service -> enter 3;\nShow list of car services -> 4;\nClose the program -> Any other symbol.");

            string choice = Console.ReadLine();

            do
            {
                switch (choice)
                {
                    case "1":
                        Work_order wo = new Work_order();
                        AddWork_order awo = new AddWork_order();
                        awo.Add(wo);
                        awo.Cost_Of_Order(wo);
                        break;
                    case "2":
                        Car_Service cs = new Car_Service();
                        AddCar_Service addCar_Service = new AddCar_Service();
                        addCar_Service.Add(cs);
                        addCar_Service._AddInList(cs);
                        break;
                    case "3":
                        Service serv = new Service();
                        AddService addService = new AddService();
                        addService.Add(serv);
                        addService._AddInList(serv);
                        break;
                    case "4":
                        List_Of_Car_Services locs = new List_Of_Car_Services();
                        locs.ShowList();
                        break;
                    default:
                        Console.WriteLine("The program has ended.");
                        break;
                }

            } while (true);
        }
    }
}
