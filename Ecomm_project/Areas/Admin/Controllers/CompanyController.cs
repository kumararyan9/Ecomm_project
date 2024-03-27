using Ecomm_project.DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly iUnitOfWork _unitofwork;
        public CompanyController(iUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
