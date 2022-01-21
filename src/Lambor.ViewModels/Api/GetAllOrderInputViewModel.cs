using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lambor.Entities;

namespace Lambor.ViewModels.Api
{
    public class GetAllOrderInputViewModel
    {

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public string Address { get; set; }
    }
}
