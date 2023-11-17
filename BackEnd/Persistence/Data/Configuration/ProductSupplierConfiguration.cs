using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ProductSupplierConfiguration : IEntityTypeConfiguration<ProductSupplier>
    {
        public void Configure(EntityTypeBuilder<ProductSupplier> builder)
        {
            builder.ToTable("productsupplier");

            builder.HasKey(k => new {k.IdProductFk, k.IdSupplierFk});

            builder.Property(e => e.SupplierPrice)
            .HasColumnType("double");

            builder.HasOne(p => p.Products)
            .WithMany(p => p.ProductsSuppliers)
            .HasForeignKey(p => p.IdProductFk);

            builder.HasOne(p => p.Suppliers)
            .WithMany(p => p.ProductsSuppliers)
            .HasForeignKey(p => p.IdSupplierFk);
        }
    }
}