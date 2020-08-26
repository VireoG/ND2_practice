using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework.Interfaces
{
    public interface IWorkOrder<T> where T : class
    {
        public T Add(T model);

        public decimal CostOfOrder(T model);
    }
}
