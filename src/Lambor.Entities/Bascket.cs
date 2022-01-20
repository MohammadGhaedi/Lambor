using Lambor.Entities.AuditableEntity;
using Lambor.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.Entities
{
    public class Basket
    {
        public int Count { get; set; }


        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
