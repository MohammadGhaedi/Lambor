using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Identity
{
    public class BascketVeiwModel
    {

        [Display(Name = "تعداد")]
        public int Count { get; set; }


        [Display(Name = "کاربر")]
        public int UserId { get; set; }
        [Display(Name = "کاربر")]
        public string UserName { get; set; }

        [Display(Name = "محصول")]
        public int ProductId { get; set; }
        [Display(Name = "محصول")]
        public string ProductName { get; set; }
    }
}
