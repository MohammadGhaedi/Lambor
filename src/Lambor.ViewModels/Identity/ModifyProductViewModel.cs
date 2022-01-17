using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Identity
{
    public class ModifyProductViewModel
    {

        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Required(ErrorMessage ="*")]
        public long Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "*")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        public int BrandId { get; set; }

        public IFormFile ProductImage { get; set; }
    }
}
