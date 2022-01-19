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
    public class BascketConfiguration : IEntityTypeConfiguration<Bascket>
    {
        public void Configure(EntityTypeBuilder<Bascket> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(bascket => bascket.Count).IsRequired();

            builder.HasKey(table => new
            {
                table.Id,
                table.ProductId,
                table.UserId
            });
            builder.HasOne(p => p.User)
           .WithMany(x => x.Basckets)
           .HasForeignKey(x => x.UserId);

            builder.HasOne(p => p.Product)
          .WithMany(x => x.Basckets)
          .HasForeignKey(x => x.ProductId);
        }
    }
}
