using Lesson2_Homework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson2_Homework.Knots
{
    public class Engine : IKnots
    {      
        public string EngineType { get; set; }
     
        public int ValueOfPistols { get; set; }

        public decimal OilTubSize { get; set; }

        public string Type { get => Type; set => Type = nameof(Engine); }

        public void AddKnot(Car car)
        {
            Engine engine = new Engine();
            car.Knots.Add(engine);
        }
    }
}
