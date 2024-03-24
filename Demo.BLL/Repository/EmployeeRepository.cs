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
    public class EmployeeRepository : IEmployeeRepository
    {

        private MVCAPP_DbContext dbContext;

        public EmployeeRepository(MVCAPP_DbContext dbContext)
        {
            this.dbContext = dbContext;
            //dbContext = new MVCAPP_DbContext();
        }
        public int Add(Employee employee)
        {
            dbContext.Add(employee);
           
            return dbContext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
           dbContext.Remove(employee);
           return dbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return dbContext.Employees.ToList();
        }

        public Employee GetById(int id)
        {
           return dbContext.Employees.Where(E => E.EmployeeId == id).FirstOrDefault();
        }

        public int Update(Employee employee)
        {
           dbContext.Update(employee);

            return dbContext.SaveChanges();
        }
    }
}
