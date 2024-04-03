using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Repository;
using Demo.DAL.Models;

namespace Demo.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork , IAsyncDisposable
    {
        private readonly MVCAPP_DbContext dbContext;

        private Hashtable ObjectRepository;

        public UnitOfWork(MVCAPP_DbContext dbContext)
        {
            //EmployeeRepository= new EmployeeRepository(dbContext);
            //DepartmentRepository= new DepartmentRepository(dbContext);
            this.dbContext = dbContext;
            ObjectRepository = new Hashtable();
        }
        //public IEmployeeRepository EmployeeRepository { get; set ; }
        //public IDepartmentRepository DepartmentRepository { get ; set; }

        public async Task<int> Complete()
        {
            return await dbContext.SaveChangesAsync();
        }

        //public void Dispose()
        //=>dbContext.Dispose();

        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var Key= typeof(T).Name;

            if (!ObjectRepository.ContainsKey(Key))
            {
                if (Key == nameof(Employee)) {
                   var  repository = new EmployeeRepository(dbContext);
                    ObjectRepository.Add(Key, repository);
                }
                else
                {
                   var repository = new GenericRepository<T>(dbContext);
                    ObjectRepository.Add(Key, repository);
                }
               

            }

            return new GenericRepository<T>(dbContext);
        }
    }
}
