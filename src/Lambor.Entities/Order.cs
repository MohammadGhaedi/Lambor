using Lambor.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public string CostumerName { get; set; }
        public string CostumerPhone { get; set; }
        public string CostumerAddress { get; set; }
        public string Description { get; set; }

        public DateTime OrderDateTime { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }


    }

    public enum OrderStatus
    {
        NotRegisterd = 0,
        Registered = 1
    }

}
