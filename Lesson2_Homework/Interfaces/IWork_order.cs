using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework.Interfaces
{
    public interface IWork_order<T> where T : class
    {
        public T Add(T model);

        public decimal Cost_Of_Order(T model);
    }
}
