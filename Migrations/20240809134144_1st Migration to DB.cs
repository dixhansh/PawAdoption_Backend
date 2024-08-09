using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawAdoption_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _1stMigrationtoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "682b344a-bef7-4df5-bb00-c280459ebe57", new DateTime(2024, 8, 9, 13, 41, 43, 985, DateTimeKind.Utc).AddTicks(3523), "AQAAAAIAAYagAAAAEF/hCfVe3XissTZGcpFJ6TKaPoPWPwj11HPqN1MFoFEWWX8UfHF+VELWjEFHZEk3Kw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "fb0f34dd-8133-45cc-a411-afc5db296b0f", new DateTime(2024, 8, 9, 13, 38, 4, 487, DateTimeKind.Utc).AddTicks(9851), "AQAAAAIAAYagAAAAECuEipXqZTy9UEylDHi2vUIPtJ4etTFn4EnMO0YbFQw10d5E3fHwOw3F8Vp2vinSow==" });
        }
    }
}
