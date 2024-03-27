using Dapper;
using Ecomm_project.DataAcess.Repository.IRepository;
using Ecomm_project.Models;
using Ecomm_Projects.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly iUnitOfWork _unitofWork;
        public CoverTypeController(iUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if(id==null) return View(coverType);
            DynamicParameters param = new DynamicParameters();
            param.Add("id", id.GetValueOrDefault());
            //coverType = _unitofWork.CoverType.Get(id.GetValueOrDefault());
            coverType = _unitofWork.Spcalls.OneRecord<CoverType>(SD.Proc_GetCovertype, param);
            if (coverType == null) return NotFound();
            return View(coverType);
        }
        [HttpPost]
        public IActionResult Upsert(CoverType coverType)
        {
            //if(coverType == null) return NotFound();
            //if (!ModelState.IsValid) return View(coverType);
            //if (coverType.Id == 0)
            //    _unitofWork.CoverType.Add(coverType);
            //else
            //    _unitofWork.CoverType.Update(coverType);
            //_unitofWork.Save();
            //return RedirectToAction("Index");
            if (coverType == null) return BadRequest();
            if (!ModelState.IsValid) return View(coverType);
            DynamicParameters param = new DynamicParameters();
            param.Add("name", coverType.Name);
            var storedProcedures = coverType.Id == 0 ? SD.Proc_CreateCov : SD.Proc_UpdateCov;
            if (coverType.Id != 0)
                param.Add("id", coverType.Id);
            _unitofWork.Spcalls.Execute(storedProcedures,param);
            _unitofWork.Save();
            return RedirectToAction("Index");
        }
        
        #region MyRegion
        [HttpGet]
        public IActionResult GetAll()
        {
            //return Json(new { data = _unitofWork.CoverType.GetAll() });
            return Json(new { data = _unitofWork.Spcalls.List<CoverType>(SD.Proc_GetCovertypes) });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var Coverindb = _unitofWork.CoverType.Get(id);
            DynamicParameters parameters= new DynamicParameters();
            parameters.Add("id", Coverindb.Id);
            if (Coverindb == null)
                return Json(new { success = false, message = "something Went Wrong!" });
            else
                //_unitofWork.CoverType.Remove(Coverindb);
                _unitofWork.Spcalls.Execute(SD.Proc_DeleteCov, parameters);
            _unitofWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}
