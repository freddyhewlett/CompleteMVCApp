using FH.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Street)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(g => g.Number)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(g => g.ZipCode)
                .IsRequired()
                .HasColumnType("varchar(8)"); //Para CEP. Alterar de acordo com necessidade

            builder.Property(g => g.Complement)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(g => g.Neighborhood)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(g => g.City)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(g => g.State)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Addresses");
        }
    }
}
