using FH.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FH.Data.Mappings
{
    public class GameMapping : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(g => g.Description)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(g => g.Image)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Games");
        }
    }
}
