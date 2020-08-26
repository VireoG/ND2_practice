using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework.Adds
{
    public class AddCar : IAdd<Car>
    {
        public Car Add(Car car)
        {
            Console.WriteLine("Enter your car model: \n");
            car.Model = Console.ReadLine(); ;          

            Console.WriteLine("Enter the VIN of your car: \n");
            car.VIN = Console.ReadLine(); ;

            Console.WriteLine("Enter the technical description of your machine by its components: \n");

            Console.WriteLine("Enter your car transmission: \n");
            string transmission = Console.ReadLine(); 

            Console.WriteLine("Enter your engine type: \n");
            string engine = Console.ReadLine();

            foreach (var component in car.Knots)
            {
                component.Transmission = transmission;
                component.Engine = engine;
            }

            return car;
        }
    }
}
