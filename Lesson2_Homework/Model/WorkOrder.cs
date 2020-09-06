using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;

namespace Lesson2_Homework
{
    public class WorkOrder
    {
        public Client Customer { get; set; }

        public Car CarInOrder { get; set; }      
        
        public List<IServices> Services { get; set; }

        public decimal CostOfOrder { get; set; }
    }
}
