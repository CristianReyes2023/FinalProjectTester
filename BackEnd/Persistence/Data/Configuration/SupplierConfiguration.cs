using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("supplier");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("int").HasMaxLength(11);

            builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
        }
    }
}