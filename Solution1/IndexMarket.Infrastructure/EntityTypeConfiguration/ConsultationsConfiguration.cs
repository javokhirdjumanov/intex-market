using IndexMarket.Domain.Constant;
using IndexMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndexMarket.Infrastructure;
public sealed class ConsultationsConfiguration 
    : IEntityTypeConfiguration<Consultation>
{
    public void Configure(EntityTypeBuilder<Consultation> builder)
    {
        builder.ToTable(TableNames.Consultations);

        builder.HasKey(con => con.Id);

        builder
            .HasOne(c => c.Order)
            .WithOne()
            .HasForeignKey<Consultation>(c => c.Order_Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}