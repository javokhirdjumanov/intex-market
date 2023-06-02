using IndexMarket.Domain.Constant;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndexMarket.Infrastructure.EntityTypeConfiguration;
public sealed class SiteConfiguration : IEntityTypeConfiguration<Site>
{
    public void Configure(EntityTypeBuilder<Site> builder)
    {
        builder
            .ToTable(TableNames.Site);

        builder
            .HasKey(t => t.Id);

        builder
            .Property(s => s.PhoneNumber)
            .HasMaxLength(100)
            .IsRequired(true);

        builder
            .Property(s => s.JobTime)
            .HasMaxLength(100)
            .IsRequired(true);

        builder
            .Property(s => s.TelegrammLink)
            .HasMaxLength(255)
            .IsRequired(true);

        builder
            .Property(s => s.InstagramLink)
            .HasMaxLength(255)
            .IsRequired(true);

        builder
            .HasOne(s => s.Address)
            .WithOne()
            .HasForeignKey<Site>(s => s.Address_Id)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasData(Generation());
    }
    private List<Site> Generation()
    {
        return new List<Site>
        {
            new Site
            {
                Id = Guid.Parse("ac56836e-0345-4f01-a883-47f39e32e079"),
                PhoneNumber = "907880021",
                JobTime = "1-20",
                TelegrammLink = "t.mejavokhirdjumanov",
                InstagramLink = "isnta.mejavokhirdjumanov",
                Address_Id = Guid.Parse("bc56836e-0345-4f01-a883-47f39e32e079")
            }
        };
    }
}
