using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ClientAddressConfiguration : IEntityTypeConfiguration<ClientAddress>
    {
        public void Configure(EntityTypeBuilder<ClientAddress> builder)
        {
            builder.ToTable("clientaddress");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("int").HasMaxLength(11);

            builder.Property(e => e.MainNumber)
            .HasColumnType("int")
            .HasMaxLength(11);

            builder.Property(e => e.Letter)
            .HasMaxLength(50);

            builder.Property(e => e.Bis)
            .HasMaxLength(50);

            builder.Property(e => e.SecLet)
            .HasMaxLength(50);

            builder.Property(e => e.Cardinal)
            .HasMaxLength(50);

            builder.Property(e => e.SecNum)
            .HasMaxLength(50);

            builder.Property(e => e.SecCard)
            .HasMaxLength(50);

            builder.Property(e => e.Complet)
            .HasMaxLength(50);

            builder.Property(e => e.PosCod)
            .HasMaxLength(50);

            builder.HasOne(p => p.Cities)
            .WithMany(p => p.ClientsAddresses)
            .HasForeignKey(p => p.IdCityFk);

            builder.HasOne(p => p.Clients)
            .WithOne(b => b.ClientsAddresses)
            .HasForeignKey<ClientAddress>(p => p.IdClientFk);

        }
    }
}