using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var employee = _employeeRepository.GetById(id.Value);
            if (employee is null) return NotFound();

            return View(ViewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest();

            //var employee = _employeeRepository.GetById(id.Value);
            //if (employee is null) return NotFound();

            //return View(department);
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (id != employee.EmployeeId) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }

            return View(employee);
        }

        //public IActionResult Delete()
        //{
        //    return View();
        //}

        #region  Delete Operation
        //  [HttpGet]
        // [ValidateAntiForgeryToken]
        // public IActionResult Delete([FromRoute] int? id)
        // {
        //     return Details(id , "Delete");
        // }
        //
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public IActionResult Delete(Department department )
        // {
        //     try
        //     {
        //      _departmentRepository.Delete(department);
        //         return RedirectToAction(nameof(Index));
        //     
        //     }
        //
        //     catch (System.Exception ex)
        //     {
        //         ModelState.AddModelError (string.Empty ,ex.Message);
        //       return View(ex.Message);
        //         throw;
        //     }
        // }
        // 
        #endregion
    }
}
