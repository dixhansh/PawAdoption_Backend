using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawAdoption_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _2stMigrationtoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdoptionApplication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdopterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationStatus = table.Column<int>(type: "int", nullable: false),
                    ReferenceCheck = table.Column<bool>(type: "bit", nullable: false),
                    ReasonOfRejection = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsFeePaid = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedByAdmin = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProcessedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdoptionApplication_AspNetUsers_AdopterId",
                        column: x => x.AdopterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdoptionApplication_AspNetUsers_ProcessedByAdmin",
                        column: x => x.ProcessedByAdmin,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AdoptionApplication_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "8134a06c-bea7-42be-a4ec-e7436b1c589a", new DateTime(2024, 8, 10, 2, 41, 29, 842, DateTimeKind.Utc).AddTicks(3421), "AQAAAAIAAYagAAAAEEUjSvuAvgLcncOXtnXAXw4kPPj90w2t58TyUPj/yzUxpn5xK844oJTBXELw2j7VmQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionApplication_AdopterId",
                table: "AdoptionApplication",
                column: "AdopterId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionApplication_PetId",
                table: "AdoptionApplication",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionApplication_ProcessedByAdmin",
                table: "AdoptionApplication",
                column: "ProcessedByAdmin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptionApplication");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "682b344a-bef7-4df5-bb00-c280459ebe57", new DateTime(2024, 8, 9, 13, 41, 43, 985, DateTimeKind.Utc).AddTicks(3523), "AQAAAAIAAYagAAAAEF/hCfVe3XissTZGcpFJ6TKaPoPWPwj11HPqN1MFoFEWWX8UfHF+VELWjEFHZEk3Kw==" });
        }
    }
}
