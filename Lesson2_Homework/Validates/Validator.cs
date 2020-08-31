using Lesson2_Homework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson2_Homework
{
    public class Validator : IValidator<WorkOrder>
    {
        public string engine;
   
        public bool Check(WorkOrder workOrder)
        {
            var valid = true;

            if (workOrder.CarInOrder == null || workOrder.Customer == null || workOrder.OrderServicesList == null)
            {
                Console.WriteLine("Not all required data has been entered.");
                valid = false;
            }

            foreach (var component in workOrder.CarInOrder.Knots)
            {
                engine = component.Engine;
            }

            if (engine == "Electrical")
            {
                foreach (var wos in workOrder.OrderServicesList)
                {
                    if (wos.Description == "Replacing filters and oils")
                        valid = false;
                }
            }

            if (engine == "Gas" || engine == "Petrol" || engine == "Diesel")
            {
                foreach (var wos in workOrder.OrderServicesList)
                {
                    if (wos.Description == "Battery Replacement")
                        valid = false;
                }
            }

            if (workOrder.CarInOrder.VIN.Length != 17)
                valid = false;

            return valid;
        }
    }
}
