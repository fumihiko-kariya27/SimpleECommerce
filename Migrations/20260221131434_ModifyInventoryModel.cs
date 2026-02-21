using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleECommerce.Migrations
{
    /// <inheritdoc />
    public partial class ModifyInventoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Products_ProductModelId_ProductModelCategoryId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_ProductModelId_ProductModelCategoryId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "ProductModelCategoryId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "ProductModelId",
                table: "Inventories");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Inventories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Products_Id_CategoryId",
                table: "Inventories",
                columns: new[] { "Id", "CategoryId" },
                principalTable: "Products",
                principalColumns: new[] { "Id", "CategoryId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Products_Id_CategoryId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Inventories");

            migrationBuilder.AddColumn<int>(
                name: "ProductModelCategoryId",
                table: "Inventories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductModelId",
                table: "Inventories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductModelId_ProductModelCategoryId",
                table: "Inventories",
                columns: new[] { "ProductModelId", "ProductModelCategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Products_ProductModelId_ProductModelCategoryId",
                table: "Inventories",
                columns: new[] { "ProductModelId", "ProductModelCategoryId" },
                principalTable: "Products",
                principalColumns: new[] { "Id", "CategoryId" });
        }
    }
}
