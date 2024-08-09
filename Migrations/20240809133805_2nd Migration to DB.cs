using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawAdoption_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _2ndMigrationtoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "fb0f34dd-8133-45cc-a411-afc5db296b0f", new DateTime(2024, 8, 9, 13, 38, 4, 487, DateTimeKind.Utc).AddTicks(9851), "AQAAAAIAAYagAAAAECuEipXqZTy9UEylDHi2vUIPtJ4etTFn4EnMO0YbFQw10d5E3fHwOw3F8Vp2vinSow==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a05844c2-98ac-4dff-ae4b-33f6dde753f9", "AQAAAAIAAYagAAAAEPBN9AvR1BLzG++qKr+QM+kl3huSsMoiYk0zd07+vjlqkdYx19lr8fLJspHeJzE1ug==" });
        }
    }
}
