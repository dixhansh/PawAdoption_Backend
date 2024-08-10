using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawAdoption_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _3rdMigrationtoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionApplication_Pets_PetId",
                table: "AdoptionApplication");

            migrationBuilder.CreateTable(
                name: "AdoptionBill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdoptionApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdopterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdoptionFee = table.Column<double>(type: "float", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedByAdmin = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionBill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdoptionBill_AdoptionApplication_AdoptionApplicationId",
                        column: x => x.AdoptionApplicationId,
                        principalTable: "AdoptionApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdoptionBill_AspNetUsers_AdopterId",
                        column: x => x.AdopterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdoptionBill_AspNetUsers_ProcessedByAdmin",
                        column: x => x.ProcessedByAdmin,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "e4f3ef39-f5f2-4edc-9068-da4fbd85c031", new DateTime(2024, 8, 10, 4, 13, 46, 60, DateTimeKind.Utc).AddTicks(7581), "AQAAAAIAAYagAAAAENsT3BE8fMrYIzsYX4rTHwNZtYWKVAnNj/+AkuEp69lspITjQ569BA80/yN2HjJQ7w==" });

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionBill_AdopterId",
                table: "AdoptionBill",
                column: "AdopterId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionBill_AdoptionApplicationId",
                table: "AdoptionBill",
                column: "AdoptionApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionBill_ProcessedByAdmin",
                table: "AdoptionBill",
                column: "ProcessedByAdmin");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionApplication_Pets_PetId",
                table: "AdoptionApplication",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionApplication_Pets_PetId",
                table: "AdoptionApplication");

            migrationBuilder.DropTable(
                name: "AdoptionBill");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "8134a06c-bea7-42be-a4ec-e7436b1c589a", new DateTime(2024, 8, 10, 2, 41, 29, 842, DateTimeKind.Utc).AddTicks(3421), "AQAAAAIAAYagAAAAEEUjSvuAvgLcncOXtnXAXw4kPPj90w2t58TyUPj/yzUxpn5xK844oJTBXELw2j7VmQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionApplication_Pets_PetId",
                table: "AdoptionApplication",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
