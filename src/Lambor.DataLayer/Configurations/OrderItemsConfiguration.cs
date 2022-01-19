using Lambor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.DataLayer.Configurations
{
    public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.HasKey(p => p.Id);
            builder.Property(orderItem => orderItem.Count).IsRequired();

            builder.HasOne(p => p.Order)
           .WithMany(x => x.OrderItems)
           .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Product)
          .WithMany(x => x.OrderItems)
          .HasForeignKey(x => x.ProductId);

        }
    }
}
