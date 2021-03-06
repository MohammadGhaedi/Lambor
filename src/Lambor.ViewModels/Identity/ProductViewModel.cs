using cloudscribe.Web.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Identity
{
    public class ProductViewModel 
    {
        [HiddenInput]
        public int Id { get; set; }
        [Display(Name="نام")]
        public string Name { get; set; }
        [Display(Name = "قیمت")]
        public long Price { get; set; }
        [Display(Name = "شرح")]
        public string Description { get; set; }
        [Display(Name = "عکس")]
        public string Image { get; set; }
        [Display(Name = "دسته بندی")]
        public int CategoryId { get; set; }
        [Display(Name = "دسته بندی")]
        public string CategoryName { get; set; }
        [Display(Name = "برند")]
        public int BrandId { get; set; }
        [Display(Name = "برند")]
        public string BrandName { get; set; }

    }

}
