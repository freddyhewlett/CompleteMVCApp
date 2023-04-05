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
    public class DeveloperMapping : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(g => g.Document)
                .IsRequired()
                .HasColumnType("varchar(14)"); // Para CNPJ. Alterar de acordo com a necessidade

            // 1 : 1 => Developer : Address
            builder.HasOne(f => f.Address)
                .WithOne(a => a.Developer);

            // 1 : N => Developer : Products
            builder.HasMany(g => g.Games)
                .WithOne(g => g.Developer)
                .HasForeignKey(d => d.DeveloperId);

            builder.ToTable("Developers");
        }
    }
}
