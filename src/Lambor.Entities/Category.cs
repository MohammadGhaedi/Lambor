using System.Collections.Generic;
using Lambor.Entities.AuditableEntity;

namespace Lambor.Entities
{
    public class Category : IAuditableEntity
    {
        public int Id { get; set; }

        public Category()
        {
            Products = new HashSet<Product>();
        }

        public Category(string name):this()
        {
            Name = name;
        }

        public string Name { get; set; }


        public virtual ICollection<Product> Products { get; set; }
    }
}