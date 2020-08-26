using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework.Interfaces
{
    public interface IAddAndList<T>
    {
        public T Add(T model);

        public void AddInList(T model);
    }
}
