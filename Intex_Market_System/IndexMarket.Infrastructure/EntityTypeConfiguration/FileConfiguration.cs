using IndexMarket.Domain.Constant;
using IndexMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndexMarket.Infrastructure;
public sealed class FileConfiguration 
    : IEntityTypeConfiguration<FileModel>
{
    public void Configure(EntityTypeBuilder<FileModel> builder)
    {
        builder.ToTable(TableNames.Files);

        builder.HasKey(file => file.Id);

        builder
            .Property(f => f.Type)
            .IsRequired(true);

        builder
            .Property(f => f.FileName)
            .IsRequired(true);
    }
}
