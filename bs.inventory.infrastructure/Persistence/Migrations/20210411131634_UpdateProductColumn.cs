using Microsoft.EntityFrameworkCore.Migrations;

namespace bs.inventory.infrastructure.Persistence.Migrations
{
    public partial class UpdateProductColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductRef",
                table: "Product",
                newName: "Reference");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Product",
                newName: "ProductRef");
        }
    }
}
