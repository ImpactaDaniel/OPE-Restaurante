using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurante.Infra.Migrations
{
    public partial class seedscustomer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CustomerAddresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "City", "State" },
                values: new object[] { "São Paulo", "SP" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CustomerAddresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "City", "State" },
                values: new object[] { null, null });
        }
    }
}
