using System;
using System.Collections.Generic;
using System.Text;
using Lesson2_Homework.Adds;
using Lesson2_Homework.Model;

namespace Lesson2_Homework.Interfaces
{
    public interface ILists<T> where T : class
    {
        public List<T> _AddList(T Model);

        public void ShowList();
    }
}
