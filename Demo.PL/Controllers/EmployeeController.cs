using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repository;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Emit;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository departmentRepository;
        private readonly IUnitOfWork unitOfWork; 
        private readonly IMapper mapper;

        public EmployeeController(/*IEmployeeRepository employeeRepository ,IDepartmentRepository departmentRepository */
                              IUnitOfWork unitOfWork,IMapper mapper) //Ask CLR that Create object from class implement interface IUnitOfWork
        {
            this.unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //this.departmentRepository = departmentRepository;
            this.mapper = mapper;
        }

        public IActionResult Index(string ValueName) 
        {
            var employees = Enumerable.Empty<Employee>();
            var employeeRepo = unitOfWork.Repository<Employee>() as EmployeeRepository;

            if (string.IsNullOrEmpty(ValueName)) 
            {
                employees = employeeRepo.GetAll();

            }

            else { employees = (IEnumerable<Employee>)employeeRepo.SearchByName(ValueName); }

            var MappedEmp = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(MappedEmp);
        }

        //public IActionResult Search(string name)
        //{
        //    var empSResult = _employeeRepository.SearchByName(name);

        //    if (empSResult is not null) { return View(empSResult); }

        //    return RedirectToAction(nameof(Index));

        //}

        [HttpGet]
        public IActionResult Create() 
        {
        //    var departs = departmentRepository.GetAll();

        //    if (departs == null)
        //    {
        //        departs = new List<Department>(); // Create an empty list
        //    }

        //    else { ViewBag.Departs = departs; }
            
            
            
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel VMemployee) 
        {
            #region Mnual Mapping
            //Manual Mapping
            //var Mapper = new Employee()
            //{
            //    Name = VMemployee.Name,
            //   Address = VMemployee.Address,
            //   Salary = VMemployee.Salary,
            //   IsActive = VMemployee.IsActive,
            //    Email = VMemployee.Email,
            //    Gender = VMemployee.Gender,
            //    PhoneNumber = VMemployee.PhoneNumber,
            //    DeptId = VMemployee.DeptId,
            //    department =VMemployee.department
            //}; 
            // consider Also Manual Mapping
            // Employee Mapper = (Employee) VMemployee; //Casting Failed Not Have Explicit overloading to can make cast  but canot make this in model 
            #endregion

            if (ModelState.IsValid)
            {
                //Auto Mapping

                var Mapperemp = mapper.Map<EmployeeViewModel, Employee>(VMemployee);
                unitOfWork.Repository<Employee>().Add(Mapperemp);


                ////2.Update emppployee
                //  emp = unitOfWork.EmployeeRepository.Update(Mapperemp);
                //

                ///3.Delte
                /// emp = unitOfWork.EmployeeRepository.Delete(Mapperemp);

                unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(VMemployee);
            }
        }

        [HttpGet]
       // [ValidateAntiForgeryToken]
        public IActionResult Details([FromRoute]int? id ,string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var emp= unitOfWork.Repository<Employee>().GetById(id.Value);
            var MappedEmp = mapper.Map<Employee, EmployeeViewModel>(emp);

            if (emp is null) return NotFound();

            return View(ViewName, MappedEmp);
        }

        [HttpGet]
       // [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id) 
        {
            return Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel VMemployee , [FromRoute] int id)
        {
            if(id != VMemployee.Id) return BadRequest();

            if (ModelState.IsValid) 
            {
                try
                {
                    var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(VMemployee);
                    unitOfWork.Repository<Employee>().Update(MappedEmp);
                    unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                     ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }

            return View(VMemployee);

        }

        [HttpGet]
       // [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id) 
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeViewModel VMemployee , [FromRoute] int? id) 
        {
            if (VMemployee.Id!= id) return BadRequest();

            if (ModelState.IsValid) 
            {
                try
                {
                    var MappedEmployee = mapper.Map<EmployeeViewModel, Employee>(VMemployee);
                    unitOfWork.Repository<Employee>().Delete(MappedEmployee);
                    unitOfWork.Complete(); //Save changes in database
                    return RedirectToAction(nameof(Index));

                }

                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                   
                    throw;
                }

            }
            return View(VMemployee);
        }
    }

}
