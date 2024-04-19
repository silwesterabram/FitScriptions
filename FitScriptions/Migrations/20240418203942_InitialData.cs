using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitScriptions.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b91413f0-e7c0-4a0d-8676-0d1b64e2c1f6", null, "User", "USER" },
                    { "ecd37281-cfff-422e-b91d-75c9aac7d90c", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Gyms",
                columns: new[] { "GymId", "Address", "ClosingTime", "Name", "OpeningTime" },
                values: new object[,]
                {
                    { new Guid("c5726a1d-35a0-46ca-8175-6adf282ee3dc"), "1234 Gold St", new TimeSpan(0, 23, 0, 0, 0), "Gold GYM", new TimeSpan(0, 6, 0, 0, 0) },
                    { new Guid("c910f34b-0b83-4e27-bc43-c5969f636c78"), "91011 Planet St", new TimeSpan(0, 23, 0, 0, 0), "Planet Fitness", new TimeSpan(0, 5, 0, 0, 0) },
                    { new Guid("f08cc4f9-61b3-4c53-9910-8190034d20f8"), "5678 Agora St", new TimeSpan(0, 22, 0, 0, 0), "Agora Fitness", new TimeSpan(0, 7, 0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b91413f0-e7c0-4a0d-8676-0d1b64e2c1f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecd37281-cfff-422e-b91d-75c9aac7d90c");

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "GymId",
                keyValue: new Guid("c5726a1d-35a0-46ca-8175-6adf282ee3dc"));

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "GymId",
                keyValue: new Guid("c910f34b-0b83-4e27-bc43-c5969f636c78"));

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "GymId",
                keyValue: new Guid("f08cc4f9-61b3-4c53-9910-8190034d20f8"));
        }
    }
}
