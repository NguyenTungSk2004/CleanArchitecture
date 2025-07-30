using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class InitProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
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

            migrationBuilder.CreateTable(
                name: "ProductPriceTiers",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPriceTiers", x => new { x.ProductId, x.Id });
                    table.ForeignKey(
                        name: "FK_ProductPriceTiers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ProductPriceTiers");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
