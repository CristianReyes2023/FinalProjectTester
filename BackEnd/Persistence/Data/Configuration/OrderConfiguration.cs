using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("int").HasMaxLength(11);

            builder.Property(e => e.OrderDate)
            .HasColumnType("date");

            builder.Property(e => e.DeadlineDate)
            .HasColumnType("date");

            builder.Property(e => e.ExpectedDate)
            .HasColumnType("date");

            builder.Property(e => e.Comments)
            .HasMaxLength(100);

            builder.HasOne(p => p.StatesOrders)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.IdStateOrderFk);

            builder.HasOne(p => p.Clients)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.IdClientFk);
        }
    }
}