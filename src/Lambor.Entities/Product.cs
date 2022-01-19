using Lambor.Entities.AuditableEntity;
using System.Collections.Generic;

namespace Lambor.Entities
{
    public class Product : IAuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }


        public virtual ICollection<Bascket> Basckets { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}