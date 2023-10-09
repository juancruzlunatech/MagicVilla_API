using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreateDate", "Details", "ImageUrl", "Name", "Ocuppancy", "Rate", "Sqft", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 10, 6, 3, 31, 8, 39, DateTimeKind.Local).AddTicks(9808), "asdfasdfasdfgfdgdfg", "", "Royal Villa ", 5, 200.0, 500, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", new DateTime(2023, 10, 6, 3, 31, 8, 39, DateTimeKind.Local).AddTicks(9831), "asdfasdfasdfgfdgdfg", "", "Royal Villa", 5, 200.0, 500, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", new DateTime(2023, 10, 6, 3, 31, 8, 39, DateTimeKind.Local).AddTicks(9833), "Escape to the serene beauty of the mountains with our Retreat Cabin. Nestled in the heart of nature, this cozy cabin is perfect for those seeking a peaceful and rustic getaway.", "", "Mountain Retreat Cabin", 4, 175.0, 800, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "", new DateTime(2023, 10, 6, 3, 31, 8, 39, DateTimeKind.Local).AddTicks(9841), "Experience the tranquility of the ocean with our Beachfront Bungalow. Enjoy stunning sunsets and direct access to the beach in this cozy and relaxing accommodation.", "", "Beachfront Bungalow", 2, 250.0, 700, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
