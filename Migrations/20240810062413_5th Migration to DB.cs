using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawAdoption_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _5thMigrationtoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "9b1177fa-b75a-468f-8e54-de1c4f16ba3a", new DateTime(2024, 8, 10, 6, 24, 12, 387, DateTimeKind.Utc).AddTicks(3882), "AQAAAAIAAYagAAAAECGp7aIZy8e/5YvgpjYdyA7cnNJgLM0+Ev45B9q0ojRA6FB6u4AiVFoejxMo2iRYuQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "274ec416-6ea6-4bcf-9e76-bb2565322cd2", new DateTime(2024, 8, 10, 4, 48, 17, 28, DateTimeKind.Utc).AddTicks(4013), "AQAAAAIAAYagAAAAEHGWgYg4Y7cK8/3mufqwN6omHuTnsz51sZIUX0rhHYl5FuoDqEI2x5VQUz73MYcQpQ==" });
        }
    }
}
