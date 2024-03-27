using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_project.Models.ViewModel
{
    public class ProductVm
    {
        //Product Product { get; set; }
        //IEnumerable<SelectListItem> CategoryList { get; set; }
        //IEnumerable<SelectListItem> CoverTypeList { get; set; }
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }
    }
}
