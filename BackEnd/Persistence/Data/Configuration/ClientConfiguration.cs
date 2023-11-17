using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("client");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("int").HasMaxLength(11);

            builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(e => e.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);

            builder.Property(e => e.Fax)
            .IsRequired()
            .HasMaxLength(15);

            builder.Property(e => e.CreditLimit)
            .HasColumnType("double");

            builder.HasOne(p => p.Employees)
            .WithMany(p => p.Clients)
            .HasForeignKey(p => p.IdEmployeeFk);

            builder.HasOne(p => p.ContactsClients)
            .WithMany(p => p.Clients)
            .HasForeignKey(p => p.IdContactCliFk);
        }
    }
}