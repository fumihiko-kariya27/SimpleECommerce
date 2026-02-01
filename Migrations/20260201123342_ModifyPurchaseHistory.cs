using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleECommerce.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPurchaseHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePointHistories_Users_CustomerId1",
                table: "PurchasePointHistories");

            migrationBuilder.DropIndex(
                name: "IX_PurchasePointHistories_CustomerId1",
                table: "PurchasePointHistories");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "PurchasePointHistories");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "PurchasePointHistories",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePointHistories_CustomerId",
                table: "PurchasePointHistories",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePointHistories_Users_CustomerId",
                table: "PurchasePointHistories",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePointHistories_Users_CustomerId",
                table: "PurchasePointHistories");

            migrationBuilder.DropIndex(
                name: "IX_PurchasePointHistories_CustomerId",
                table: "PurchasePointHistories");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "PurchasePointHistories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "PurchasePointHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePointHistories_CustomerId1",
                table: "PurchasePointHistories",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePointHistories_Users_CustomerId1",
                table: "PurchasePointHistories",
                column: "CustomerId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
