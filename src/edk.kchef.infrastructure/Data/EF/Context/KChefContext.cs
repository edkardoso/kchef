using edk.Kchef.Domain.Products;
using edk.Kchef.Infrastructure.Data.EF.Configuration;
using Microsoft.EntityFrameworkCore;

namespace edk.Kchef.Infrastructure.Data.EF.Context
{
    public class KChefContext : DbContext
    {
        public KChefContext(DbContextOptions<KChefContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
