using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Identity
{
    public class OrderItemViewModel
    {
        public long Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public long OrderId { get; set; }
    }

    public class OrderItemDisplayViewModel: OrderItemViewModel
    {
        public string ProductName{ get; set; }
    }
}
