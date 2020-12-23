using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Services.IServices
{
    public interface ICRUD<T> where T: class
    {
        Task Save(T model);

        Task EditSave(T model);

        Task Delete(T model);
    }
}
