using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson2_Homework
{
    public class CreateOrder : IWorkOrder<WorkOrder>
    {
        public WorkOrder Create(WorkOrder workOrder)
        {
            Car car = new Car();
            if (car == null)
                throw new Exception("Insufficient data");
            else
            workOrder.CarInOrder = car;


            Client client = new Client();
            if (client == null)
                throw new Exception("Insufficient data");
            else
                workOrder.Customer = client;

            return workOrder;
        }

        public decimal CostOfOrder(WorkOrder workOrder)
        {                     

            foreach(var services in workOrder.Services)
            {
               decimal a = services.Cost(workOrder.CarInOrder.Knots);
                workOrder.CostOfOrder += a;
            }

            return workOrder.CostOfOrder;
        }
    }
}
