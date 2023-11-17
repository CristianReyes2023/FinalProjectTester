using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(11);

            builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(e => e.Dimensiones)
            .HasColumnType("double");

            builder.Property(e => e.Description)
            .HasMaxLength(100);

            builder.Property(e => e.Stock)
            .HasColumnType("int");

            builder.Property(e => e.PriceSale)
            .HasColumnType("double");

            builder.HasOne(p => p.RangersProducts)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.IdRangerFk);
        }
    }
}