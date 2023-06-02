using IndexMarket.Domain.Constant;
using IndexMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndexMarket.Infrastructure.EntityTypeConfiguration;
public sealed class CategoriesConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(TableNames.Category);

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Title)
            .HasMaxLength(100)
            .IsRequired(true);

        //builder.HasData(Generation());
    }
    private List<Category> Generation()
    {
        return new List<Category>()
        {
            new Category
            {
                Id = Guid.Parse("bd56836e-0345-4f01-a883-47f39e32e079"),
                Title = "Shishirilgan"
            }
        };
    }
}
