using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework
{
    public class WorkOrder
    {
        public Client Customer { get; set; }

        public Car CarInOrder { get; set; }

        public List<Service> OrderServicesList { get; set; }       
        
        public decimal CostOfOrder { get; set; }
    }
}
