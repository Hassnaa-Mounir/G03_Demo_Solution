using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var departments = await unitOfWork.Repository<Department>().GetAllAsync(); 


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
        public async Task<IActionResult> Create(Department department)
        {
            if(ModelState.IsValid)
            {
                unitOfWork.Repository<Department>().Add(department); // # of rows effected
               var result= await unitOfWork.Complete();

                if (result > 0) { TempData["Message"] = "Department Will Created"; }
                return RedirectToAction(nameof(Index));
            }
            return View(department);    
        }

        public async Task<IActionResult> Details(int? id , string ViewName ="Details")
        {
            if (id is null) return BadRequest();

            var department = await unitOfWork.Repository<Department>().GetAsync(id.Value);
            if (department is null) return NotFound();

            return View(ViewName ,department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest();

            //var department = _departmentRepository.GetById(id.Value);
            //if (department is null) return NotFound();

            //return View(department);
            return await Details(id ,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department ,[FromRoute] int id)
        {
            if (id != department.Id) return BadRequest();

            if(ModelState.IsValid)
            {
                try 
                {
                    unitOfWork.Repository<Department>().Update(department);
                   await unitOfWork.Complete();
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
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Department department ,[FromRoute]int? id)
        {
            if (id != department.Id) return BadRequest();
            try
            {
                unitOfWork.Repository<Department>().Delete(department);
               await unitOfWork.Complete();
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
