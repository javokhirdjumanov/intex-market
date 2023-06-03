using IndexMarket.Domain.Constant;
using IndexMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndexMarket.Infrastructure.EntityTypeConfiguration;
public sealed class ProductShapeConfiguration : IEntityTypeConfiguration<ProductShape>
{
    public void Configure(EntityTypeBuilder<ProductShape> builder)
    {
        builder.ToTable(TableNames.ProductShape);

        builder.HasKey(p => p.Id);

        builder
            .Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired(true);
    }
}
