using cloudscribe.Web.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Identity
{
    public class ProductViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Name { get; set; }
        public long Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }

}
