using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;

namespace Lesson2_Homework.Model
{
    public class CarService
    {
        public string ServiceName { get; set; }

        public List<Service> Services { get; set; }
    }
}
