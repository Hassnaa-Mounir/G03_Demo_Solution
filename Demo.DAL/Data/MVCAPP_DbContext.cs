using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data
{
    public class MVCAPP_DbContext : DbContext
    {

        public MVCAPP_DbContext(DbContextOptions<MVCAPP_DbContext> options) : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Server = . ; Database = MVC_APP ; Trusted_Connection = true ; Encrypted = false;"); // EF Core

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<Employee>().HasOne(e=>e.department).WithMany(d=>d.Employees).HasForeignKey(d=>d.DeptId)/*.OnDelete(DeleteBehavior.Cascade)*/;

            modelBuilder.Entity<Employee>().Property<bool>("IsActive");


        }


    }
}
