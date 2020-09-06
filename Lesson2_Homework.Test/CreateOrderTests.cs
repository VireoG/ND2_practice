using Bogus;
using Lesson2_Homework.Knots;
using Lesson2_Homework.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson2_Homework.Test
{
    public class CreateOrderTests
    {       
        private Faker<Car> carFaker;
        private Faker<Client> clientFaker;

        [OneTimeSetUp]
        public void Setup()
        {
            carFaker = new Faker<Car>()
                .RuleFor(c => c.Model, f => f.Vehicle.Model() + f.Vehicle.Manufacturer())
                .RuleFor(c => c.VIN, f => f.Vehicle.Vin());

            clientFaker = new Faker<Client>()
                .RuleFor(cl => cl.Name, f => f.Name.FirstName())
                .RuleFor(cl => cl.Surname, f => f.Name.LastName())
                .RuleFor(cl => cl.Gender, f => f.Random.Word());
        }

        [Test]
        public void WorkOrder_CostOfOrder_Value()
        {
            //Arrange
            Engine engine = new Engine();
            engine.EngineType = "Petrol";
            engine.OilTubSize = 2;
            engine.ValueOfPistols = 6;
            engine.Type = "Engine";

            Transmission transmission = new Transmission();
            transmission.NumberOfShaftsInGearBox = 5;

            ChangingOil changingOil = new ChangingOil();
            changingOil.PricePerLiter = 30;
            changingOil.CostOfService = 0;

            GearBoxReplacing gearBoxReplacing = new GearBoxReplacing();
            gearBoxReplacing.DefaultPrice = 40;
            gearBoxReplacing.MarkupPerFiveShuftsType = 2;
            gearBoxReplacing.CostOfService = 0;

            ReplacementPistons replacementPistons = new ReplacementPistons();
            replacementPistons.PricePerPistol = 2;
            replacementPistons.CostOfService = 0;

            WorkOrder workOrder = new WorkOrder();
            workOrder.CarInOrder.Knots.Add(engine);
            workOrder.CarInOrder.Knots.Add(transmission);
            workOrder.Services.Add(replacementPistons);
            workOrder.CostOfOrder = 0;
            workOrder.Services.Add(changingOil);
            workOrder.Services.Add(gearBoxReplacing);
            workOrder.Customer = clientFaker;
            workOrder.CarInOrder = carFaker;
            CreateOrder createOrder = new CreateOrder();

            //Act
            createOrder.Create(workOrder);          
            workOrder.CostOfOrder = createOrder.CostOfOrder(workOrder);

            //Assert
            Assert.AreEqual(172, workOrder.CostOfOrder);
        }
    }
}
