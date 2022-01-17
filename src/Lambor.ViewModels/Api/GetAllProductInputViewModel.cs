using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Api
{
    public  class GetAllProductInputViewModel
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string Filter { get; set; }
    }
}
