using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndexMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deletePhotoLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoLink",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoLink",
                table: "Products",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
