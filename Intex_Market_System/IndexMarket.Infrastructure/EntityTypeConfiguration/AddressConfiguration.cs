using IndexMarket.Domain.Constant;
using IndexMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndexMarket.Infrastructure.EntityTypeConfiguration;
public sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(TableNames.Address);

        builder.HasKey(add => add.Id);

        builder
            .Property(add => add.Country)
            .HasMaxLength(100)
            .IsRequired(true);

        builder
            .Property(add => add.City)
            .HasMaxLength(100)
            .IsRequired(false);

        builder
            .Property(add => add.Region)
            .HasMaxLength(100)
            .IsRequired(false);

        builder
            .Property(add => add.Street)
            .HasMaxLength(100)
            .IsRequired(false);

        builder
            .Property(add => add.PostalCode)
            .IsRequired(false);

        //builder.HasData(Generation());
    }
    private List<Address> Generation()
    {
        return new List<Address>
        {
            new Address
            {
                Id = Guid.Parse("bc56836e-0345-4f01-a883-47f39e32e079"),
                Country = "Uzbekistan"
            }
        };
    }
}
