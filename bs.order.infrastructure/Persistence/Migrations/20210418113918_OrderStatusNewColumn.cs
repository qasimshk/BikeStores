using Microsoft.EntityFrameworkCore.Migrations;

namespace bs.order.infrastructure.Persistence.Migrations
{
    public partial class OrderStatusNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JsonDeliveryAddress",
                table: "OrderState",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonDeliveryAddress",
                table: "OrderState");
        }
    }
}
