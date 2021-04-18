using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bs.inventory.infrastructure.Persistence.Migrations
{
    public partial class AddOrderRefColumnToInventoryStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderRef",
                table: "InventoryStatus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderRef",
                table: "InventoryStatus");
        }
    }
}
