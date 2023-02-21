using edk.Kchef.Domain.Entities.Users;
using edk.Kchef.Domain.Products;
using edk.Kchef.Infrastructure.Data.EF.Configuration;
using Microsoft.EntityFrameworkCore;

namespace edk.Kchef.Infrastructure.Data.EF.Context
{
    public class KChefContext : DbContext
    {
        public KChefContext(DbContextOptions<KChefContext> options)
            : base(options)
        { 
            this.Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());


        }
    }
}
