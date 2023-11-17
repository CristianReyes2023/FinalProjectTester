using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ContactClientConfiguration : IEntityTypeConfiguration<ContactClient>
    {
        public void Configure(EntityTypeBuilder<ContactClient> builder)
        {
            builder.ToTable("contactclient");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("int").HasMaxLength(11);

            builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(30);

            builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(30);
        }
    }
}