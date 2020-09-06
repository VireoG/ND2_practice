using System;
using System.Collections.Generic;
using System.Text;


namespace Lesson2_Homework.Interfaces
{
    public interface IWorkOrder<T> where T : class
    {
        public decimal CostOfOrder(T model);
    }
}
