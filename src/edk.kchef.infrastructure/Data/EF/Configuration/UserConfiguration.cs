using edk.Kchef.Domain.Common;
using edk.Kchef.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using System;

namespace edk.Kchef.Infrastructure.Data.EF.Configuration;

public class UserConfiguration : EntityBaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure("Users", builder);

        builder.HasIndex(e => e.Login).IsUnique();
        builder.Property(e => e.Login)
                 .HasMaxLength(SizeFields.SIZE_4)
                 .IsRequired(); 

        builder.OwnsOne(e => e.Name, setup =>
        {
            setup.Property(vo => vo.FirstName).HasMaxLength(SizeFields.SIZE_3).IsRequired();
            setup.Property(vo => vo.MiddleName).HasMaxLength(SizeFields.SIZE_3);
            setup.Property(vo => vo.LastName).HasMaxLength(SizeFields.SIZE_3);
        });

        builder.HasIndex(e => e.Email).IsUnique();
        builder.Property(e => e.Email)
            .HasMaxLength(SizeFields.SIZE_4)
            .IsRequired(); 

        builder.Property(e => e.Password)
         .HasMaxLength(SizeFields.SIZE_6)
         .IsRequired();

        builder.Property(e => e.ExpirationDate)
            .IsRequired();

        builder.Property(e => e.Blocked)
            .HasDefaultValue(false);

    }
}
