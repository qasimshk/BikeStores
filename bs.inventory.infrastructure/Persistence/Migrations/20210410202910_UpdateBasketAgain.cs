using Microsoft.EntityFrameworkCore.Migrations;

namespace bs.inventory.infrastructure.Persistence.Migrations
{
    public partial class UpdateBasketAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Basket");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "BasketItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "BasketItem");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Basket",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
