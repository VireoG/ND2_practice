using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework.Interfaces
{
    public interface IAdd_And_List<T>
    {
        public T Add(T model);

        public void _AddInList(T model);
    }
}
