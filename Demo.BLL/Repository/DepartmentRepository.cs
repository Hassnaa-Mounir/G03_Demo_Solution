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
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {

      //  private MVCAPP_DbContext _dbContext;

        public DepartmentRepository(MVCAPP_DbContext dbContext) :base(dbContext)
        {
            
        }
       
    }
}
