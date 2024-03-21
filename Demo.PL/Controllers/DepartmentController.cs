using Demo.BLL.Interfaces;
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
    }
}
