using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork ,IDisposable
    {
        private readonly MVCAPP_DbContext dbContext;

        public UnitOfWork(MVCAPP_DbContext dbContext)
        {
            EmployeeRepository= new EmployeeRepository(dbContext);
            DepartmentRepository= new DepartmentRepository(dbContext);
            this.dbContext = dbContext;
        }
        public IEmployeeRepository EmployeeRepository { get; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set; }

        public int Complete()
        {
          return  dbContext.SaveChanges();
        }

        public void Dispose()
        =>dbContext.Dispose();
    }
}
