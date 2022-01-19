using Lambor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.DataLayer.Mappings
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(bascket => bascket.CostumerName).HasMaxLength(450).IsRequired();
            builder.Property(bascket => bascket.CostumerPhone).HasMaxLength(11);

            builder.HasKey(table => new
            {
                table.Id,
                table.UserId
            });

            builder.HasOne(p => p.User)
           .WithMany(x => x.Orders)
           .HasForeignKey(x => x.UserId);


            builder.Property(e => e.OrderStatus).HasMaxLength(50).HasConversion(
               v => v.ToString(),
               v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));


        }

        
    }
}
