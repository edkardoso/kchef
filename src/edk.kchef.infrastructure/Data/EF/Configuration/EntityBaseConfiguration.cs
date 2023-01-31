using edk.Kchef.Domain.Common.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace edk.Kchef.Infrastructure.Data.EF.Configuration
{
    public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);

        protected void Configure(string tableName, EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(e => e.Id).HasName($"PKey_{tableName[..^1]}Id");
            builder.Property(e => e.Deleted);

        }

    }
}
