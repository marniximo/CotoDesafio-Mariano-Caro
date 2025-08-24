using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CotoDesafio.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    CarModelName = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Tax = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.CarModelName);
                });

            migrationBuilder.CreateTable(
                name: "DistributionCenters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    CarChassisNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CarModelName = table.Column<string>(type: "TEXT", nullable: false),
                    DistributionCenterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.CarChassisNumber);
                    table.ForeignKey(
                        name: "FK_Sales_CarModels_CarModelName",
                        column: x => x.CarModelName,
                        principalTable: "CarModels",
                        principalColumn: "CarModelName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_DistributionCenters_DistributionCenterId",
                        column: x => x.DistributionCenterId,
                        principalTable: "DistributionCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "CarModelName", "Price", "Tax" },
                values: new object[,]
                {
                    { "OffRoad", 12500m, 0m },
                    { "Sedan", 8000m, 0m },
                    { "Sport", 18200m, 7m },
                    { "SUV", 9500m, 0m }
                });

            migrationBuilder.InsertData(
                table: "DistributionCenters",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Centro Norte" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Centro Sur" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Centro Este" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Centro Oeste" }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "CarChassisNumber", "CarModelName", "Date", "DistributionCenterId" },
                values: new object[,]
                {
                    { "11114ejemplo", "OffRoad", new DateTime(205, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222") },
                    { "1211ejemplo", "Sport", new DateTime(205, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222") },
                    { "1223334ejemplo", "SUV", new DateTime(205, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111") },
                    { "1234ejemplo", "Sedan", new DateTime(205, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111") },
                    { "1235ejemplo", "Sedan", new DateTime(205, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222") },
                    { "1236ejemplo", "Sport", new DateTime(205, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111") },
                    { "1237ejemplo", "OffRoad", new DateTime(205, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222") },
                    { "1238ejemplo", "SUV", new DateTime(205, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111") },
                    { "1239ejemplo", "Sedan", new DateTime(205, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CarModelName",
                table: "Sales",
                column: "CarModelName");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DistributionCenterId",
                table: "Sales",
                column: "DistributionCenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropTable(
                name: "DistributionCenters");
        }
    }
}
