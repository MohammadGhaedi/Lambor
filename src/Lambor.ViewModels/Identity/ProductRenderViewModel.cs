using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Identity
{
    public  class ProductRenderViewModel
    {
        public ProductViewModel Product { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Brands { get; set; }


    }
}
