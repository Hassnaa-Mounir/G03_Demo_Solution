using Demo.BLL.Interfaces;
using Demo.BLL.Repository;
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
            var employees= _employeeRepository.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create() 
        {  
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Employee employee) 
        {
            if(ModelState.IsValid)
            {
                var emp = _employeeRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(employee);
            }
        }

        [HttpGet]
       // [ValidateAntiForgeryToken]
        public IActionResult Details([FromRoute]int? id ,string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var emp= _employeeRepository.GetById(id.Value);

            if (emp is null) return NotFound();

            return View(ViewName , emp);
        }

        [HttpGet]
       // [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id) 
        {
            return Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee , [FromRoute] int id)
        {
            if(id != employee.Id) return BadRequest();

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

        [HttpGet]
       // [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id) 
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee , [FromRoute] int? id) 
        {
            if (employee.Id!= id) return BadRequest();

            if (ModelState.IsValid) 
            {
                try
                {
                    _employeeRepository.Delete(employee);
                    return RedirectToAction(nameof(Index));

                }

                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                   
                    throw;
                }

            }
            return View(employee);
        }
    }

}
