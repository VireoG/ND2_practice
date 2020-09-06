using Lesson2_Homework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson2_Homework.Knots
{
    public class Transmission : IKnots
    {

      //  public string TransmissionType { get; set; }

        //public string Clutch { get; set; }

       // public string Differential{ get; set; }

        public int NumberOfShaftsInGearBox { get; set; }

       // public string TypeOfGearbox { get; set; }

        public string Type { get => ((IKnots)transmission).Type; set => ((IKnots)transmission).Type = nameof(Transmission); }

        public Transmission transmission;
        public Transmission()
        {    
            transmission = new Transmission();
        }

        public void AddKnot(Car car)
        {
            car.Knots.Add(transmission); 
        }
    }
}
