using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurante.Infra.Migrations
{
    public partial class firstaccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FirstAccess",
                table: "Employees",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstAccess",
                table: "Employees");
        }
    }
}
