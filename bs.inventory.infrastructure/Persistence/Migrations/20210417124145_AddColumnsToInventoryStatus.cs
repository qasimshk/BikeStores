using Microsoft.EntityFrameworkCore.Migrations;

namespace bs.inventory.infrastructure.Persistence.Migrations
{
    public partial class AddColumnsToInventoryStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BasketPrice",
                table: "InventoryStatus",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "JsonBasketItems",
                table: "InventoryStatus",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasketPrice",
                table: "InventoryStatus");

            migrationBuilder.DropColumn(
                name: "JsonBasketItems",
                table: "InventoryStatus");
        }
    }
}
