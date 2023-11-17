using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employee");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("int").HasMaxLength(11);

            builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(e => e.LastNameOne)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(e => e.LastNameTwo)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(e => e.Extension)
            .IsRequired()
            .HasMaxLength(10);

            builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

            builder.HasOne(p => p.Bosses)
            .WithMany(p => p.Employees)
            .HasForeignKey(p => p.IdBoosFk);

            builder.HasOne(p => p.Offices)
            .WithMany(p => p.Employees)
            .HasForeignKey(p => p.IdOfficeFk);
            
            builder.HasOne(p => p.PositionsEmployees)
            .WithMany(p => p.Employees)
            .HasForeignKey(p => p.IdPositionFk);
        }
    }
}