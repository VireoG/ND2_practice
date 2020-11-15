using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Services
{
    public interface ICRUD<T> where T: class
    {
        public Task Save(T model);

        public Task EditSave(T model);

        public Task Delete(T model);
    }
}
