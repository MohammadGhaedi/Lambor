using Lambor.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Api
{
    public class OrderItemsViewModel
    {
        public long Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public long OrderId { get; set; }
    }
}
