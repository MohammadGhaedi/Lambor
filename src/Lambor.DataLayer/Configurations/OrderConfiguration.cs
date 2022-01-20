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
            builder.HasKey(table => table.Id);

            builder.Property(Basket => Basket.CostumerName).HasMaxLength(450).IsRequired();
            builder.Property(Basket => Basket.CostumerPhone).HasMaxLength(11);


            builder.HasOne(p => p.User)
           .WithMany(x => x.Orders)
           .HasForeignKey(x => x.UserId).IsRequired();

        }


    }
}
