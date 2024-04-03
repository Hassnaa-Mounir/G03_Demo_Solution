using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class  
    {
        private protected readonly MVCAPP_DbContext dbContext;

        public GenericRepository(MVCAPP_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(T item)
        => dbContext.Add(item);
 

        public void Delete(T item)
       =>   dbContext.Remove(item);
           

      

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
              return (IEnumerable<T>) await dbContext.Employees.Include(e => e.department).AsNoTracking().ToListAsync();
            else
            return await dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        => await dbContext.FindAsync<T>(id);


        public void Update(T item)
       =>   dbContext.Update(item);


          
    }
}
