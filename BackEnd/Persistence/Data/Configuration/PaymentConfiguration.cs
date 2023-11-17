using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payment");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(11);

            builder.Property(e => e.DatePayment)
            .HasColumnType("date");

            builder.Property(e => e.Total)
            .HasColumnType("double");

            builder.HasOne(p => p.Clients)
            .WithMany(p => p.Payments)
            .HasForeignKey(p => p.IdClientFk);

            builder.HasOne(p => p.PaymentsMethods)
            .WithMany(p => p.Payments)
            .HasForeignKey(p => p.IdPaymenMetFk);
        }
    }
}