using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework
{
    public class Work_order
    {
        public Client _Client { get; set; }

        public Car _Car { get; set; }

        public List<Service> _Services { get; set; }       
        
        public decimal Cost_Of_Order { get; set; }
    }
}
