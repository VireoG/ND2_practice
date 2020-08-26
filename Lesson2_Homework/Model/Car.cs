using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework
{
    public class Car
    {
        public string Model { get; set; }

        public string VIN { get; set; }

        public List<CarInsides> Knots { get; set; }
    }
}
