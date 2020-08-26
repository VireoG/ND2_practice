using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Model;
using System.Linq;

namespace Lesson2_Homework
{
    public class AddCarService : IAddAndList<CarService>
    {
        private ListOfServices los;

        public AddCarService()
        {
            los = new ListOfServices();
        }

        public CarService Add(CarService CS)
        {
            
            Console.WriteLine("Enter the name of your Car Service:");
            CS.ServiceName = Console.ReadLine();

            Console.WriteLine("Select the services that you want to add to the service from the list, enter the Index of the desired service;\nTo end the selection, enter word <exit>.\n");            
            los.ShowList();        

            string so;

            do
            {
                so = Console.ReadLine();
                bool success = Int32.TryParse(so, out int s);

                if(success == true)
                CS.Services.Add(los.servicelist.ElementAt(s));

            } while (so != "exit");

            return CS;
        }

        public void AddInList(CarService CS)
        {
            var add = new ListOfCarServices();
            add.AddList(CS);
        }
    }
}
