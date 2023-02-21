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

        builder.Property(e => e.Login)
                 .HasMaxLength(SizeFields.SIZE_4)
                 .IsRequired(); // TODO: Garantir que o login seja único

        builder.OwnsOne(e => e.Name, setup =>
        {
            setup.Property(vo => vo.FirstName).HasMaxLength(SizeFields.SIZE_3).IsRequired();
            setup.Property(vo => vo.MiddleName).HasMaxLength(SizeFields.SIZE_3);
            setup.Property(vo => vo.LastName).HasMaxLength(SizeFields.SIZE_3);
        });

        builder.Property(e => e.Email)
            .HasMaxLength(SizeFields.SIZE_4)
            .IsRequired(); // TODO: Garantir que o email seja único

        builder.Property(e => e.Password)
         .HasMaxLength(SizeFields.SIZE_6)
         .IsRequired();

        builder.Property(e => e.ExpirationDate)
            .IsRequired();

        builder.Property(e => e.Blocked)
            .HasDefaultValue(false);

    }
}
