using System;
using System.Text.Json;
using System.Text;
using Bogus;
using Bogus.Extensions;
using Lesson2_Homework;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;
using System.Collections.Generic;
using System.Linq;

namespace Lesson2_Homework.Console
{
    public class Program
    {
        static void Main()
        {
         
            var serviceIndex = 0;

            var serviceFaker = new Faker<Service>()
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

            Faker<Car> carFaker = new Faker<Car>()
                .RuleFor(c => c.Model, f => f.Vehicle.Model() + f.Vehicle.Manufacturer())
                .RuleFor(c => c.VIN, f => f.Vehicle.Vin())
                .RuleFor(c => c.Knots, carInsidesFaker.Generate(1).ToList());

            var clientFaker = new Faker<Client>()
                .RuleFor(cl => cl.Name, f => f.Name.FirstName())
                .RuleFor(cl => cl.Surname, f => f.Name.LastName())
                .RuleFor(cl => cl.Gender, f => f.Random.Word());

            var carServiceFaker = new Faker<CarService>()
                .RuleFor(cs => cs.ServiceName, f => f.Company.CompanyName())
                .RuleFor(cs => cs.Services, serviceFaker.Generate(30).ToList());                

            var workOrderFaker = new Faker<WorkOrder>()
                .RuleFor(wo => wo.CarInOrder, carFaker.Generate())
                .RuleFor(wo => wo.Customer, clientFaker.Generate())
                .RuleFor(wo => wo.OrderServicesList, serviceFaker.Generate(5).ToList())
                .RuleFor(wo => wo.CostOfOrder, f => f.Random.Decimal());

            var testData = workOrderFaker.Generate(10);

            var json = JsonSerializer.Serialize(testData);

            System.Console.WriteLine(json);
        }
    }
}
