using IndexMarket.Domain.Constant;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndexMarket.Infrastructure.EntityTypeConfiguration
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableNames.User);

            builder.HasKey(u => u.Id);

            builder
            .Property(user => user.FirstName)
            .HasMaxLength(100)
            .IsRequired(true);

            builder
                .Property(user => user.LastName)
                .HasMaxLength(100)
                .IsRequired(false);

            builder
                .Property(user => user.PhoneNumber)
                .HasMaxLength(30)
                .IsRequired(true);

            builder
                .Property(user => user.Email)
                .HasMaxLength(255)
                .IsRequired(true);

            builder
                .Property(user => user.PasswordHash)
                .HasMaxLength(256)
                .IsRequired(true);

            builder
                .Property(user => user.Salt)
                .HasMaxLength(100)
                .IsRequired(true);

            builder
                .Property(user => user.CreatedAt)
                .IsRequired(true);

            builder
                .Property(u => u.Role)
                .IsRequired(true);

            builder
                .HasOne(user => user.Address)
                .WithOne()
                .HasForeignKey<User>(user => user.Address_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(GenerateUsers());
        }
        private List<User> GenerateUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = Guid.Parse("bc56836e-0345-4f01-a883-47f39e32e079"),
                    FirstName = "Javokhir",
                    Role = UserRoles.Admin,
                    PhoneNumber = "+998-90-000-22-11",
                    Email = "javokhir@gmail.com",
                    PasswordHash = "0803",
                    Salt = Guid.NewGuid().ToString(),
                    Address_Id = Guid.Parse("bc56836e-0345-4f01-a883-47f39e32e079")
                }
            };
        }
    }
}
