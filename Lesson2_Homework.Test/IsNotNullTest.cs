using Bogus;
using Lesson2_Homework.Model;
using Lesson2_Homework.Adds;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Lesson2_Homework.Test
{
    public class IsNotNullTest
    {
        private Faker<WorkOrder> workOrderFaker;
        private Faker<Car> carFaker;
        private Faker<Service> serviceFaker;
        private Faker<Client> clientFaker;
        private Faker<CarService> carServiceFaker;


        [OneTimeSetUp]
        public void Setup()
        {
            var serviceIndex = 0;

             serviceFaker = new Faker<Service>()
                .RuleFor(s => s.Index, serviceIndex++)
                .RuleFor(s => s.Description, f => f.Lorem.Text())
                .RuleFor(s => s.Cost, f => f.Random.Decimal());

            var carInsidesFaker = new Faker<CarInsides>()
                .RuleFor(ci => ci.Engine, f => f.Random.Word())
                .RuleFor(ci => ci.Chassis, f => f.Random.Word())
                .RuleFor(ci => ci.Brakes, f => f.Random.Word())
                .RuleFor(ci => ci.Transmission, f => f.Random.Word())
                .RuleFor(ci => ci.Сarcase, f => f.Random.Word())
                .RuleFor(ci => ci.Suspension, f => f.Random.Word())
                .RuleFor(ci => ci.ElectricalEquipment, f => f.Random.Word());

            carFaker = new Faker<Car>()
                .RuleFor(c => c.Model, f => f.Vehicle.Model() + f.Vehicle.Manufacturer())
                .RuleFor(c => c.VIN, f => f.Vehicle.Vin())
                .RuleFor(c => c.Knots, carInsidesFaker.Generate(1).ToList());

            clientFaker = new Faker<Client>()
                .RuleFor(cl => cl.Name, f => f.Name.FirstName())
                .RuleFor(cl => cl.Surname, f => f.Name.LastName())
                .RuleFor(cl => cl.Gender, f => f.Random.Word());

            carServiceFaker = new Faker<CarService>()
                .RuleFor(cs => cs.ServiceName, f => f.Company.CompanyName())
                .RuleFor(cs => cs.Services, serviceFaker.Generate(30).ToList());            

            workOrderFaker= new Faker<WorkOrder>()
               .RuleFor(wo => wo.CarInOrder, carFaker.Generate())
               .RuleFor(wo => wo.Customer, clientFaker.Generate())
               .RuleFor(wo => wo.OrderServicesList, serviceFaker.Generate(5).ToList());
        }

        [Test]
        public void _IsNotNullTest() 
        {
            //Arrange        
            var addWorkOrder = new AddWorkOrder();
            var workOrder = new WorkOrder();
            var listOfCarServices = new ListOfCarServices();
            var listOfServices = new ListOfServices();
            var carService = new CarService();         

            //Act     
            workOrder.CostOfOrder = addWorkOrder.CostOfOrder(workOrderFaker);
            listOfServices.AddList(serviceFaker);
            listOfCarServices.AddList(carServiceFaker);


            //Assert
            Assert.IsNotNull(listOfServices.serviceList);
            Assert.IsNotNull(listOfCarServices.carServiceList);
            Assert.IsNotNull(workOrder.CostOfOrder);
        }
    }
}
