using Lesson2_Homework.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace Lesson2_Homework.Test
{
    public class ValidatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DataEntryCheck()
        {
            //Arrange
            WorkOrder workOrder = new WorkOrder();
            Validator validator = new Validator();
            
            workOrder.CarInOrder.VIN = "12345678912345678";
            workOrder.CarInOrder.Knots = new List<CarInsides>
            {
                new CarInsides { Engine = "Gas", Brakes = "word", Chassis = "word",
                                ElectricalEquipment = "word", Suspension = "word",
                                Transmission = "word", Ñarcase = "word"
                                }
            };
            workOrder.CarInOrder.Model = "Toyota";
            workOrder.Customer.Name = "SomeName";
            workOrder.Customer.Surname = "SomeNameToo";
            workOrder.Customer.Gender = "Male";

            workOrder.OrderServicesList = new List<Service>
            {
                new Service { Index = 1, Description = "Service", Cost = 234}
            };

            workOrder.CostOfOrder = 234m;

            //Act
            bool validate = validator.Check(workOrder);

            //Assert
            Assert.AreEqual(true, validate);
        }
    }
}