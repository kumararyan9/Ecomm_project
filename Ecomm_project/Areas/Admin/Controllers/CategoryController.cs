using Dapper;
using Ecomm_project.DataAcess.Repository.IRepository;
using Ecomm_project.Models;
using Ecomm_Projects.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly iUnitOfWork _unitOfWork;
        public CategoryController(iUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id) {
            Category category = new Category();
            if(id==null) return View(category);
            //category= _unitOfWork.Category.Get(id.GetValueOrDefault());
            DynamicParameters param = new DynamicParameters();
            param.Add("id", id.GetValueOrDefault());
            category=_unitOfWork.Spcalls.OneRecord<Category>(SD.Proc_getCategory, param);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult Upsert(Category category) {
         if(category== null) return NotFound();
         if(!ModelState.IsValid) { return View(category); }
         DynamicParameters parameters= new DynamicParameters();
          parameters.Add("name", category.Name);
          var storedProcedure = category.Id != 0 ? SD.Proc_UpdateCategory : SD.Proc_CreateCategory;
          if (category.Id != 0)
                parameters.Add("id", category.Id);
          _unitOfWork.Spcalls.Execute(storedProcedure, parameters);
            //if (category.Id == 0)
            //{
            //    _unitOfWork.Category.Add(category);
            //}
            //else
            //    _unitOfWork.Category.Update(category);
           _unitOfWork.Save();
           return RedirectToAction("Index");
        }

        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            //var categlist = _unitOfWork.Category.GetAll();
            //return Json(new { data= categlist });
            return Json(new { data = _unitOfWork.Spcalls.List<Category>(SD.Proc_getCategorys) });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
           var catgindb = _unitOfWork.Category.Get(id);
            DynamicParameters param = new DynamicParameters();
            param.Add("id",catgindb.Id);
            if(catgindb == null)
                return Json(new {success=false,message="something went wrong!!"});
            else
                //_unitOfWork.Category.Remove(catgindb);
                _unitOfWork.Spcalls.Execute(SD.Proc_DeleteCategory,param);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });
            
        }
        #endregion
    }
}
