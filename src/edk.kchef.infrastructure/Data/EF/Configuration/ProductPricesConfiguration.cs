using edk.Kchef.Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace edk.Kchef.Infrastructure.Data.EF.Configuration;

public class ProductPricesConfiguration : EntityBaseConfiguration<ProductPrice>
{
   
    public override void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        base.Configure("ProductPrices", builder);

        builder.Property(e => e.Date)
            .IsRequired();

        builder.Property(e => e.Price)
            .IsRequired();

    }
}


