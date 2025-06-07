using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaiphongTech.Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class initDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Quantity1 = table.Column<int>(type: "int", nullable: true),
                    SellingPrice1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity2 = table.Column<int>(type: "int", nullable: true),
                    SellingPrice2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity3 = table.Column<int>(type: "int", nullable: true),
                    SellingPrice3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity4 = table.Column<int>(type: "int", nullable: true),
                    SellingPrice4 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(200)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    BarCode = table.Column<string>(type: "varchar(200)", nullable: true),
                    TaxRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WaitingTimeUntilAvailable = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    PreOrderQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitOfQuantityId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    OriginId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecoveryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BarCode",
                table: "Products",
                column: "BarCode",
                unique: true,
                filter: "[BarCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
