using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Lesson2_Homework.Knots;
using NUnit.Framework;
using Lesson2_Homework.Services;

namespace Lesson2_Homework.Test
{
    public class GearBoxReplacingTests
    {        
        [Test]
        public void GearBoxReplacing_TestOfValue()
        {
            //Arrange
            GearBoxReplacing gearBoxReplacing = new GearBoxReplacing();
            gearBoxReplacing.DefaultPrice = 10;
            gearBoxReplacing.MarkupPerFiveShuftsType = 2;
            gearBoxReplacing.MarkupPerSixShuftsType = 2;
            gearBoxReplacing.CostOfService = 0;

            Transmission transmission = new Transmission();
            transmission.NumberOfShaftsInGearBox = 5;
            transmission.Type = "Transmission";

            WorkOrder workOrder = new WorkOrder();
            workOrder.CarInOrder.Knots.Add(transmission);
            workOrder.Services.Add(gearBoxReplacing);
            //Act
            gearBoxReplacing.CostOfService = gearBoxReplacing.Cost(workOrder.CarInOrder.Knots);

            //Assert
            Assert.AreEqual(20, gearBoxReplacing.CostOfService);
        }

    }
}
