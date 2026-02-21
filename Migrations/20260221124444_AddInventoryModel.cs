using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductModelCategoryId = table.Column<int>(type: "int", nullable: true),
                    ProductModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => new { x.Id, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_Inventories_Products_ProductModelId_ProductModelCategoryId",
                        columns: x => new { x.ProductModelId, x.ProductModelCategoryId },
                        principalTable: "Products",
                        principalColumns: new[] { "Id", "CategoryId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductModelId_ProductModelCategoryId",
                table: "Inventories",
                columns: new[] { "ProductModelId", "ProductModelCategoryId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");
        }
    }
}
