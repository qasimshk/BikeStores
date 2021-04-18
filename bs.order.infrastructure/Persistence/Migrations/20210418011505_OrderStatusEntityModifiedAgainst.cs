using Microsoft.EntityFrameworkCore.Migrations;

namespace bs.order.infrastructure.Persistence.Migrations
{
    public partial class OrderStatusEntityModifiedAgainst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardDetailId",
                table: "OrderState",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "JsonCardDetails",
                table: "OrderState",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CardNumberUnFormatted",
                table: "CardDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardDetailId",
                table: "OrderState");

            migrationBuilder.DropColumn(
                name: "JsonCardDetails",
                table: "OrderState");

            migrationBuilder.DropColumn(
                name: "CardNumberUnFormatted",
                table: "CardDetail");
        }
    }
}
