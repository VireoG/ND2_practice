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
                        WorkOrder wo = new WorkOrder();
                        AddWorkOrder awo = new AddWorkOrder();
                        awo.Add(wo);
                        awo.CostOfOrder(wo);
                        break;
                    case "2":
                        CarService cs = new CarService();
                        AddCarService addCar_Service = new AddCarService();
                        addCar_Service.Add(cs);
                        addCar_Service.AddInList(cs);
                        break;
                    case "3":
                        Service serv = new Service();
                        AddService addService = new AddService();
                        addService.Add(serv);
                        addService.AddInList(serv);
                        break;
                    case "4":
                        ListOfCarServices locs = new ListOfCarServices();
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
