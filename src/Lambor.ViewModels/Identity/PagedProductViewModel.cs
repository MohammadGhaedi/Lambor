using cloudscribe.Web.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Identity
{
    public  class PagedProductViewModel
    {
        public PagedProductViewModel()
        {
            Paging = new PaginationSettings();
        }
        public PaginationSettings Paging { get; set; }
        public List<ProductViewModel> Items { get; set; }

    }
}
