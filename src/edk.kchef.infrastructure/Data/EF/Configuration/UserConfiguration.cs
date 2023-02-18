using edk.Kchef.Domain.Common;
using edk.Kchef.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace edk.Kchef.Infrastructure.Data.EF.Configuration;

public class UserConfiguration : EntityBaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure("Users", builder);

        builder.Property(e => e.Login)
                 .HasMaxLength(SizeFields.LARGE)
                 .IsRequired();

        builder.Property(e => e.FirstName)
            .HasMaxLength(SizeFields.LARGE);

        builder.Property(e => e.LastName)
            .HasMaxLength(SizeFields.EXTRA_LARGE);

        builder.Property(e => e.Email)
            .HasMaxLength(SizeFields.EXTRA_LARGE)
            .IsRequired();

        builder.Property(e => e.Password)
         .HasMaxLength(SizeFields.BIG)
         .IsRequired();

    

        builder.Property(e => e.ExpirationDate)
            .IsRequired();

        builder.Property(e => e.Blocked);

    }
}
