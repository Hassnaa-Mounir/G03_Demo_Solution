using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{ 
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{

      //  private readonly MVCAPP_DbContext _dbContext;

    public EmployeeRepository(MVCAPP_DbContext dbContext) : base(dbContext)
    {
           
    }

    public IQueryable GetByAddress(string address)
    {
        return dbContext.Employees.Where(E => E.Address == address);
    }

    public IQueryable SearchByName(string Name)
    {
      return dbContext.Employees.Where(E => E.Name.ToLower().Contains(Name.ToLower()));
    }
    }
}
