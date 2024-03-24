using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departRepository)
        {
            _departmentRepository = departRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid)
            {
                _departmentRepository.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);    
        }

        public IActionResult Details(int? id , string ViewName ="Details")
        {
            if (id is null) return BadRequest();

            var department = _departmentRepository.GetById(id.Value);
            if (department is null) return NotFound();

            return View(ViewName ,department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest();

            //var department = _departmentRepository.GetById(id.Value);
            //if (department is null) return NotFound();

            //return View(department);
            return Details(id ,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department ,[FromRoute] int id)
        {
            if (id != department.Id) return BadRequest();
            if(ModelState.IsValid)
            {
                try 
                {
                    _departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));
                } 
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty ,ex.Message);
                
                }
            }

            return View(department);
        }

        //public IActionResult Delete()
        //{
        //    return View();
        //}

        #region  Delete Operation
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department )
        {
            try
            {
                _departmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));

            }

            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(ex.Message);
                throw;
            }
        }

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
