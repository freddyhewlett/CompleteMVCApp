using FH.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FH.Data.Context
{
    public class DataDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Developer> Developers { get; set; }


        public DataDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(g => g.ClrType == typeof(string))))
            //{
            //    property.Relational().ColumnType = "varchar(100)";
            //}

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
