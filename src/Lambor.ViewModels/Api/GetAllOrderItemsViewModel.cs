using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Api
{
    public class GetAllOrderItemsViewModel
    {
        public int? ProductId { get; set; }
        public long? OrderId { get; set; }


    }
}
