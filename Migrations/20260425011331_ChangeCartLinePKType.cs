using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleECommerce.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCartLinePKType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CartLines", 
                table: "CartLines"
            );

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CartLines"
            );

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "CartLines",
                nullable: false
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartLines",
                table: "CartLines",
                column: "Id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CartLines",
                table: "CartLines"
            );

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CartLines"
            );

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CartLines",
                nullable: false
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartLines",
                table: "CartLines",
                column: "Id"
            );
        }
    }
}
