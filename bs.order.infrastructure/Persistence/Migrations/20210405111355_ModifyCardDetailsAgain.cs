using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bs.order.infrastructure.Persistence.Migrations
{
    public partial class ModifyCardDetailsAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Expiration",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "SecurityNumber",
                table: "OrderItem");

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "CardDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expiration",
                table: "CardDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SecurityNumber",
                table: "CardDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "CardDetail");

            migrationBuilder.DropColumn(
                name: "Expiration",
                table: "CardDetail");

            migrationBuilder.DropColumn(
                name: "SecurityNumber",
                table: "CardDetail");

            migrationBuilder.AddColumn<int>(
                name: "CardNumber",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Expiration",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecurityNumber",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
