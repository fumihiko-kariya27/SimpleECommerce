using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => new { x.CategoryId, x.ProductId, x.Sequence });
                    table.ForeignKey(
                        name: "FK_ProductImages_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId_ProductCategoryId",
                        columns: x => new { x.ProductId, x.ProductCategoryId },
                        principalTable: "Products",
                        principalColumns: new[] { "Id", "CategoryId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId_ProductCategoryId",
                table: "ProductImages",
                columns: new[] { "ProductId", "ProductCategoryId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");
        }
    }
}
