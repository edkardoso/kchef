using edk.Kchef.Domain.Products;
using edk.Kchef.Domain.Users;
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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());


        }
    }
}
