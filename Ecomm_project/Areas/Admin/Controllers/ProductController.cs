using Ecomm_project.DataAcess.Repository.IRepository;
using Ecomm_project.Models;
using Ecomm_project.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecomm_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly iUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(iUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Credit(int? id)
        {
            ProductVm productVm = new ProductVm()
            {
              Product=new Product(),
              CategoryList=_unitofwork.Category.GetAll().Select(cl=>new
              SelectListItem()
              {
                  Text=cl.Name,
                  Value=cl.Id.ToString()
              }),
              CoverTypeList= _unitofwork.CoverType.GetAll().Select(ct=>new SelectListItem()
              {
                  Text=ct.Name,
                  Value=ct.Id.ToString()
              })
            };
            if(id==null) return View(productVm);
            productVm.Product = _unitofwork.ProductRep.Get(id.GetValueOrDefault());
            if (productVm.Product == null) return BadRequest();
            return View(productVm);            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Credit(ProductVm productVm)
        {
            if(ModelState.IsValid)
            {
                var webrootpath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    //var productid = productVm.Product.Id.ToString();
                    var filename = Guid.NewGuid().ToString();
                    var extension= Path.GetExtension(files[0].FileName);
                    var uploads = Path.Combine(webrootpath, @"images\products");
                    if (productVm.Product.Id!=0){
                        var imageExists = _unitofwork.ProductRep.Get(productVm.Product.Id).ImageUrl;
                        productVm.Product.ImageUrl=imageExists;
                    }
                    if (productVm.Product.ImageUrl != null)
                    {
                        var imagepath = Path.Combine(webrootpath,productVm.Product.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(imagepath))
                        {
                            System.IO.File.Delete(imagepath);
                        }                          
                    }
                    using (var filestream = new FileStream(Path.Combine(uploads,filename+extension),FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    productVm.Product.ImageUrl = @"\images\products\" + filename + extension;
                }
                else
                {
                    if (productVm.Product.Id != 0)
                    {
                        var imageExists = _unitofwork.ProductRep.Get(productVm.Product.Id).ImageUrl;
                        productVm.Product.ImageUrl=imageExists;
                    }                  
                }
                if (productVm.Product.Id == 0)
                    _unitofwork.ProductRep.Add(productVm.Product);
                else
                    _unitofwork.ProductRep.Update(productVm.Product);
                _unitofwork.Save();
                return RedirectToAction("Index");
            }
            else
            {
               var productId = productVm.Product.Id;
                 productVm = new ProductVm()
                 {
                    Product = new Product(),
                    CategoryList = _unitofwork.Category.GetAll().Select(cl => new SelectListItem()
                    {
                        Text = cl.Name,
                        Value = cl.Id.ToString()
                    }),
                    CoverTypeList = _unitofwork.CoverType.GetAll().Select(ct => new SelectListItem()
                    {
                        Text = ct.Name,
                        Value = ct.Id.ToString()
                    })
                 };
                if (productId != 0)
                {
                    productVm.Product = _unitofwork.ProductRep.Get(productId);
                }
                return View(productVm);
            }
        }

        #region ApIs
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitofwork.ProductRep.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productindb = _unitofwork.ProductRep.Get(id);
            if (productindb == null)
                return Json(new { success = false, message = "SOme Eroor occured!" });
            var webrootpath = _webHostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webrootpath, productindb.ImageUrl.Trim('\\'));
            if(System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _unitofwork.ProductRep.Remove(productindb);
            _unitofwork.Save();
            return Json(new { success = true, message = " Deletion Successful" });
        }
        #endregion
    }
}
