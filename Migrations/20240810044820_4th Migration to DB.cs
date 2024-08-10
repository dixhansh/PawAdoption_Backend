using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawAdoption_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _4thMigrationtoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessedBy",
                table: "AdoptionApplication");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "274ec416-6ea6-4bcf-9e76-bb2565322cd2", new DateTime(2024, 8, 10, 4, 48, 17, 28, DateTimeKind.Utc).AddTicks(4013), "AQAAAAIAAYagAAAAEHGWgYg4Y7cK8/3mufqwN6omHuTnsz51sZIUX0rhHYl5FuoDqEI2x5VQUz73MYcQpQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProcessedBy",
                table: "AdoptionApplication",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "e4f3ef39-f5f2-4edc-9068-da4fbd85c031", new DateTime(2024, 8, 10, 4, 13, 46, 60, DateTimeKind.Utc).AddTicks(7581), "AQAAAAIAAYagAAAAENsT3BE8fMrYIzsYX4rTHwNZtYWKVAnNj/+AkuEp69lspITjQ569BA80/yN2HjJQ7w==" });
        }
    }
}
