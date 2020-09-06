using Lesson2_Homework.Knots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson2_Homework.Interfaces
{
    public interface IKnots
    {
        public string Type { get; set; }

        public void AddKnot(Car car);
    }
}
