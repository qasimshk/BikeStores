using Microsoft.EntityFrameworkCore.Migrations;

namespace bs.identity.infrastructure.Persistence.Migrations
{
    public partial class AddColumnToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Designation",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "AspNetUsers");
        }
    }
}
