using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "Capacity", "CreationDate", "Detail", "ImgageUrl", "Name", "Price", "SquareMetres", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "", 8, new DateTime(2024, 1, 28, 17, 32, 54, 744, DateTimeKind.Local).AddTicks(3009), "Increible", "", "Villa Real", 200.0, 90, new DateTime(2024, 1, 28, 17, 32, 54, 744, DateTimeKind.Local).AddTicks(3019) },
                    { 2, "", 6, new DateTime(2024, 1, 28, 17, 32, 54, 744, DateTimeKind.Local).AddTicks(3021), "Con icreible vista a la gran piscina", "", "Villa Olimpica", 100.0, 70, new DateTime(2024, 1, 28, 17, 32, 54, 744, DateTimeKind.Local).AddTicks(3021) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
