using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndexMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Wight",
                table: "Products");

            migrationBuilder.AlterColumn<double>(
                name: "Height",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Products",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bc56836e-0345-4f01-a883-47f39e32e079"),
                columns: new[] { "CreatedAt", "Salt" },
                values: new object[] { new DateTime(2023, 6, 2, 6, 49, 48, 864, DateTimeKind.Utc).AddTicks(2137), "fb4109b3-f853-439c-b06b-d5dc4e991512" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "Wight",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bc56836e-0345-4f01-a883-47f39e32e079"),
                columns: new[] { "CreatedAt", "Salt" },
                values: new object[] { new DateTime(2023, 6, 1, 9, 1, 45, 284, DateTimeKind.Utc).AddTicks(443), "c0c55de5-07a4-4d74-ae88-a314c71fd4f4" });
        }
    }
}
