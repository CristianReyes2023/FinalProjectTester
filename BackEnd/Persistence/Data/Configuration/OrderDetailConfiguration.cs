using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("orderdetail");

            builder.HasKey(k => new {k.IdOrderFk, k.IdProductFk});

            builder.Property(e => e.Quantity)
            .HasColumnType("int")
            .HasMaxLength(11);

            builder.Property(e => e.UnitPrice)
            .HasColumnType("double");

            builder.Property(e => e.LineNumber)
            .HasColumnType("int")
            .HasMaxLength(6);

            builder.HasOne(p => p.Orders)
            .WithMany(p => p.OrdersDetails)
            .HasForeignKey(p => p.IdOrderFk);

            builder.HasOne(p => p.Products)
            .WithMany(p => p.OrdersDetails)
            .HasForeignKey(p => p.IdProductFk);
        }
    }
}