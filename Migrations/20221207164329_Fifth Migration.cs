using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class FifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SoldItems",
                table: "SoldItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoldItems",
                table: "SoldItems",
                columns: new[] { "ItemID", "ReceiptID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SoldItems",
                table: "SoldItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoldItems",
                table: "SoldItems",
                column: "ItemID");
        }
    }
}
