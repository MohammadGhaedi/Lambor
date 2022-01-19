using Lambor.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.Entities
{
    public class OrderItem
    {
        public long Id { get; set; }

        public int Count { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public virtual Order Order { get; set; }
        public long OrderId { get; set; }
    }
}
