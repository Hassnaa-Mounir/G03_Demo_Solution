﻿using Demo.BLL.Interfaces;
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
    public class DepartmentRepository : IDepartmentRepository
    {

        private MVCAPP_DbContext _dbContext;

        public DepartmentRepository(MVCAPP_DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Department department)
        {
            _dbContext.Add(department);

            return _dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _dbContext.Remove(department);

            return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbContext.Departments.ToList();
        }

        public Department GetById(int id)
        {
            //var depart= _dbContext.Departments.Local.Where(D =>  D.Id ==id).FirstOrDefault();

            //if(depart is null)
            //    depart = _dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();
            //return depart;

            return _dbContext.Departments.Find(id);

        }

        public int Update(Department department)
        {
            _dbContext.Update(department);

            return _dbContext.SaveChanges();
        }
    }
}
