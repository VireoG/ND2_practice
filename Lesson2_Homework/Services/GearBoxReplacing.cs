using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Knots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lesson2_Homework.Services
{
    public class GearBoxReplacing : IServices
    {
        public decimal DefaultPrice { get; set; }

        public decimal MarkupPerFiveShuftsType { get; set; }

        public decimal MarkupPerSixShuftsType { get; set; }

        public decimal CostOfService { get; set; }      

        public decimal Cost(List<IKnots> knots)
        {
            var selected = from knot in knots
                           where knot.Type == "Transmission"
                           select knot;

            Transmission transmission = (Transmission)selected.ElementAt(0);

            if (transmission == null)
                throw new Exception("Insufficient data");

            if(transmission.NumberOfShaftsInGearBox == 5)
            {
                CostOfService = DefaultPrice * MarkupPerFiveShuftsType;
                return CostOfService;
            }

            if (transmission.NumberOfShaftsInGearBox == 6)
            {                
                CostOfService = DefaultPrice * MarkupPerSixShuftsType;
                return CostOfService;
            }

            CostOfService = DefaultPrice;

            return CostOfService;
        }
    }
}
