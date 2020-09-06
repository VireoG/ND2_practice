using Bogus;
using Lesson2_Homework.Knots;
using Lesson2_Homework.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson2_Homework.Test
{
    public class ChangingOilTests
    {
        [Test]
        public void ChangingOilTestOfValue()
        {
            //Arrange
            Engine engine = new Engine();
            engine.EngineType = "Gas";
            engine.OilTubSize = 2;
            engine.ValueOfPistols = 4;
            engine.Type = "Engine";

            ChangingOil changingOil = new ChangingOil();
            changingOil.PricePerLiter = 30;
            changingOil.CostOfService = 0;

            WorkOrder workOrder = new WorkOrder();
            workOrder.CarInOrder.Knots.Add(engine);
            workOrder.CostOfOrder = 0;

            //Act
            changingOil.CostOfService = changingOil.Cost(workOrder.CarInOrder.Knots);

            //Assert
            Assert.AreEqual(60, changingOil.CostOfService);
        }
    }
}
