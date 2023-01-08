using edk.Kchef.Domain.Common;
using edk.Kchef.Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace edk.Kchef.Infrastructure.Data.EF.Configuration;

public class ProductConfiguration : EntityBaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure("Products", builder);

        builder.Property(e => e.Name)
            .HasMaxLength(SizeFields.LARGE)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(SizeFields.EXTRA_LARGE);
        builder.Property(e => e.Unity)
            .IsRequired();

        builder.HasMany(e => e.Prices)
                .WithOne(e => e.Product);
    }
}
