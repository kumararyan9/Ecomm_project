using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_project.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ISBN { get; set; }
        [Required]
        [Range(1,1000)]
        public int ListPrice { get; set; }
        [Required]
        [Range(1,1000)]
        public int Price { get; set; }
        [Required]
        [Range(1,1000)]
        public int Price50 { get; set; }
        [Required]
        [Range(1,1000)]
        public int Price100 { get; set; }
        [Display(Name ="Image Url")]
        public string ImageUrl { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        [Display(Name = "CoverType")]
        public int CoverTypeId { get; set; }
        public CoverType CoverType { get; set; }
    }
}
