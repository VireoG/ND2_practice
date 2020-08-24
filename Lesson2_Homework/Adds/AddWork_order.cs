using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;
using System.Linq;

namespace Lesson2_Homework
{
    public class AddWork_order : IWork_order<Work_order>
    {
        private List_Of_Services los;
        private Validator validator;
        public AddWork_order()
        {
            los = new List_Of_Services();
            validator = new Validator();
        }

        public Work_order Add(Work_order work_Order)
        {            
            AddClient addClient = new AddClient();
            addClient.Add(work_Order._Client);
            
            AddCar addCar = new AddCar();
            addCar.Add(work_Order._Car);

            Console.WriteLine("\nChoose the services you need from the list, to do this, enter the index of the required service;\nTo end the selection, enter word <exit>.\n");           
            los.ShowList();

            string so;

            do
            {
                so = Console.ReadLine();
                bool success = Int32.TryParse(so, out int s);

                if (success == true)
                    work_Order._Services.Add(los.servicelist.ElementAt(s));

            } while (so != "exit");

            return work_Order;
        }  

        public decimal Cost_Of_Order(Work_order work_Order)
        {            
            work_Order.Cost_Of_Order = 0m;

            if (!validator.Check(work_Order))
            {
                Console.WriteLine("Check failed, order was rejected.");
            }
            else
            {
                Console.WriteLine("The check was passed, the order was accepted.");

                foreach (var wos in work_Order._Services)
                {
                    decimal c = wos.Cost;
                    work_Order.Cost_Of_Order = work_Order.Cost_Of_Order + c;
                }
            }

            Console.WriteLine($"The cost of your order is: {work_Order.Cost_Of_Order}");

            return work_Order.Cost_Of_Order;
        }
    }
}
