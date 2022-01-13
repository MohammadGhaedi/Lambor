using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lambor.ViewModels.Identity
{
    public class CategoryViewModel
    {
        [HiddenInput]
        public int Id { set; get; }


        [Required(ErrorMessage = "(*)")]
        [Display(Name = " دسته بندی")]
        public string Name { get; set; }
            
    }
}
