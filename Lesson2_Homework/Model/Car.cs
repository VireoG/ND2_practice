using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;

namespace Lesson2_Homework
{
    public class Car
    {
        public string Model { get; set; }

        public string VIN { get; set; }

        public List<IKnots> Knots { get; set; }
    }
}
