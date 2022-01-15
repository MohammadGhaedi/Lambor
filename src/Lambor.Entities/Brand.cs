
using System.Collections.Generic;
using Lambor.Entities.AuditableEntity;

namespace Lambor.Entities
{
    public class Brand : IAuditableEntity
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        public Brand(string name) : this()
        {
            this.Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
