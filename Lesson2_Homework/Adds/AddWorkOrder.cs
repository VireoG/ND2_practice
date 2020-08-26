using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;
using System.Linq;

namespace Lesson2_Homework
{
    public class AddWorkOrder : IWorkOrder<WorkOrder>
    {
        private ListOfServices los;
        private Validator validator;
        public AddWorkOrder()
        {
            los = new ListOfServices();
            validator = new Validator();
        }

        public WorkOrder Add(WorkOrder workOrder)
        {            
            AddClient addClient = new AddClient();
            addClient.Add(workOrder.Customer);
            
            AddCar addCar = new AddCar();
            addCar.Add(workOrder.CarInOrder);

            Console.WriteLine("\nChoose the services you need from the list, to do this, enter the index of the required service;\nTo end the selection, enter word <exit>.\n");           
            los.ShowList();

            string so;

            do
            {
                so = Console.ReadLine();
                bool success = Int32.TryParse(so, out int s);

                if (success == true)
                    workOrder.OrderServicesList.Add(los.servicelist.ElementAt(s));

            } while (so != "exit");

            return workOrder;
        }  

        public decimal CostOfOrder(WorkOrder workOrder)
        {            
            workOrder.CostOfOrder = 0m;

            if (!validator.Check(workOrder))
            {
                Console.WriteLine("Check failed, order was rejected.");
            }
            else
            {
                Console.WriteLine("The check was passed, the order was accepted.");

                foreach (var wos in workOrder.OrderServicesList)
                {
                    decimal c = wos.Cost;
                    workOrder.CostOfOrder = workOrder.CostOfOrder + c;
                }
            }

            Console.WriteLine($"The cost of your order is: {workOrder.CostOfOrder}.");

            return workOrder.CostOfOrder;
        }
    }
}
