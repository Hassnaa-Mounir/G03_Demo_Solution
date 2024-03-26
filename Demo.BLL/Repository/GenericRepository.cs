using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCAPP_DbContext dbContext;

        public GenericRepository(MVCAPP_DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(T item)
        {
            dbContext.Add(item);
            return dbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            dbContext.Remove(item);
            return dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
       => dbContext.Set<T>().ToList();

        public T GetById(int id)
        => dbContext.Set<T>().Find(id);
       

        public int Update(T item)
        {
            dbContext.Update(item);
            return dbContext.SaveChanges();
        }
    }
}
