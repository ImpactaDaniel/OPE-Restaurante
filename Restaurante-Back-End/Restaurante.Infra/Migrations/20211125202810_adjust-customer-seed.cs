using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurante.Infra.Migrations
{
    public partial class adjustcustomerseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Password" },
                values: new object[] { "teste@gmail.com", "$2b$10$JslArELBa5XKc8b8crVZ9.GN1bY90Cf7c5ibgoTj.vO3fL7pt863q" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Invoices",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Password" },
                values: new object[] { "daniel@gmail.com", "$2b$10$9QsGNTO4SNA6QqsrQRq/AutnF9I3XQLQYKv6ofHvwpuyb0.w97bZa" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
