using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawAdoption_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _6thMigrationtoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATE");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DueDate",
                table: "AdoptionBill",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "PasswordHash" },
                values: new object[] { "fdd42bd7-5e4a-485b-bbf5-da9b5ac023e1", new DateTime(2024, 8, 10, 10, 51, 24, 524, DateTimeKind.Utc).AddTicks(5131), new DateOnly(1998, 2, 10), "AQAAAAIAAYagAAAAECMr1S6APNOnMM7/FkMXZmuvoao8VTawQJo4s9spzlKznut91EZVLLvulx74lYpALA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "DATE",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "AdoptionBill",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "PasswordHash" },
                values: new object[] { "9b1177fa-b75a-468f-8e54-de1c4f16ba3a", new DateTime(2024, 8, 10, 6, 24, 12, 387, DateTimeKind.Utc).AddTicks(3882), new DateTime(1998, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAIAAYagAAAAECGp7aIZy8e/5YvgpjYdyA7cnNJgLM0+Ev45B9q0ojRA6FB6u4AiVFoejxMo2iRYuQ==" });
        }
    }
}
