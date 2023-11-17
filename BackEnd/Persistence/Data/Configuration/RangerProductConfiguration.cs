using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class RangerProductConfiguration : IEntityTypeConfiguration<RangerProduct>
    {
        public void Configure(EntityTypeBuilder<RangerProduct> builder)
        {
            builder.ToTable("rangerproduct");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(11);

            builder.Property(e => e.DescriptionText)
            .HasMaxLength(100);

            builder.Property(e => e.DescriptionHtml)
            .HasMaxLength(100);

            builder.Property(e => e.Img)
            .HasMaxLength(100);
        }
    }
}