using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        // private IDepartmentRepository _departmentRepository;

        public DepartmentController( IUnitOfWork unitOfWork)
        {
           // _departmentRepository = departRepository;
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var departments = unitOfWork.Repository<Department>().GetAll();


            ////1.ViewData

            //ViewData["Message"] = "Hello from viewData";

            ////2.ViewBag

            //ViewBag.Message = "Hello from viewBag";

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
                unitOfWork.Repository<Department>().Add(department); // # of rows effected
               var result= unitOfWork.Complete();

                if (result > 0) { TempData["Message"] = "Department Will Created"; }
                return RedirectToAction(nameof(Index));
            }
            return View(department);    
        }

        public IActionResult Details(int? id , string ViewName ="Details")
        {
            if (id is null) return BadRequest();

            var department = unitOfWork.Repository<Department>().GetById(id.Value);
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
                    unitOfWork.Repository<Department>().Update(department);
                    unitOfWork.Complete();
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
        public IActionResult Delete([FromRoute] int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department ,[FromRoute]int? id)
        {
            if (id != department.Id) return BadRequest();
            try
            {
                unitOfWork.Repository<Department>().Delete(department);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }

            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(department);
                throw;
            }
        }

        #endregion
    }
}
