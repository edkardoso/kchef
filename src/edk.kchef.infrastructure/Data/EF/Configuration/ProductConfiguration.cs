using edk.Kchef.Domain.Ordes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace edk.Kchef.Infrastructure.Data.EF.Configuration
{

    public class ProductConfiguration : EntityBaseConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure("Products", builder);

            builder.Property(e => e.Name)
                .HasMaxLength(SizeConstants.LARGE)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(SizeConstants.EXTRA_LARGE);

            builder.Property(e => e.Unidade)
                .HasColumnType("byte")
                .IsRequired();

        }
    }
}
