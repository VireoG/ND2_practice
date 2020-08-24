using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Interfaces;
using Lesson2_Homework.Model;

namespace Lesson2_Homework.Adds
{
    public class AddClient : IAdd<Client>
    {
        public Client Add(Client client)
        {
            Console.WriteLine("Enter your name:\n");
            client.Name = Console.ReadLine();
            
            Console.WriteLine("Enter your surname:\n");
            client.Surname = Console.ReadLine();

            Console.WriteLine("Enter your gender:\n");
            client.Gender = Console.ReadLine();

            return client;
        }
    }
}
