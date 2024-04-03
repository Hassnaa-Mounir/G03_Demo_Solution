using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository <T>  where T : class 
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        void Add(T item);

       void Update(T item);

        void Delete(T item);

       
    }
}
