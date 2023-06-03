using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndexMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductShapes_Name",
                table: "ProductShapes");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Title",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductShapes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "Id", "Address_Id", "CreatedAt", "InstagramLink", "JobTime", "PhoneNumber", "TelegrammLink", "UpdatedAt" },
                values: new object[] { new Guid("ac56836e-0345-4f01-a883-47f39e32e079"), new Guid("bc56836e-0345-4f01-a883-47f39e32e079"), new DateTime(2023, 6, 3, 11, 26, 48, 188, DateTimeKind.Utc).AddTicks(4662), "isnta.me//javokhirdjumanov", "9.AM - 7.PM", "+998-90-788-00-21", "t.me//javokhirdjumanov", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bc56836e-0345-4f01-a883-47f39e32e079"),
                columns: new[] { "CreatedAt", "Email", "FirstName", "PasswordHash", "PhoneNumber", "Salt" },
                values: new object[] { new DateTime(2023, 6, 3, 11, 26, 48, 189, DateTimeKind.Utc).AddTicks(1482), "javokhir@gmail.com", "Javokhir", "0803", "+998-90-000-22-11", "3517d44c-3716-4f4c-9615-a2b603dea66b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sites",
                keyColumn: "Id",
                keyValue: new Guid("ac56836e-0345-4f01-a883-47f39e32e079"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductShapes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bc56836e-0345-4f01-a883-47f39e32e079"),
                columns: new[] { "CreatedAt", "Email", "FirstName", "PasswordHash", "PhoneNumber", "Salt" },
                values: new object[] { new DateTime(2023, 6, 3, 5, 22, 9, 418, DateTimeKind.Utc).AddTicks(599), "toxirjon@gmail.com", "Toxirjon", "12345", "931234567", "4295a9c7-1394-4a0f-9783-c39bc013c06b" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductShapes_Name",
                table: "ProductShapes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Title",
                table: "Categories",
                column: "Title",
                unique: true);
        }
    }
}
