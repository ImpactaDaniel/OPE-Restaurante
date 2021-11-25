using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurante.Infra.Migrations
{
    public partial class seedscustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId",
                table: "CustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerPhone_PhoneId",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneId",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "CustomerAddresses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "CustomerPhone",
                columns: new[] { "Id", "DDD", "PhoneNumber" },
                values: new object[] { 1, "11", "910703000" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BirthDate", "CreatedDate", "Document", "Email", "FirstAccess", "Name", "Password", "PhoneId", "Type", "UpdatedDate" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "10845441051", "daniel@gmail.com", false, "Daniel", "$2b$10$9QsGNTO4SNA6QqsrQRq/AutnF9I3XQLQYKv6ofHvwpuyb0.w97bZa", 1, 3, null });

            migrationBuilder.InsertData(
                table: "CustomerAddresses",
                columns: new[] { "Id", "CEP", "City", "CustomerId", "District", "Number", "State", "Street" },
                values: new object[] { 1, "02998190", null, 1, "City Jaraguá", "180", null, "Rua Ângelo Benincori" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerPhone_PhoneId",
                table: "Customers",
                column: "PhoneId",
                principalTable: "CustomerPhone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId",
                table: "CustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerPhone_PhoneId",
                table: "Customers");

            migrationBuilder.DeleteData(
                table: "CustomerAddresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CustomerPhone",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "PhoneId",
                table: "Customers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "CustomerAddresses",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerPhone_PhoneId",
                table: "Customers",
                column: "PhoneId",
                principalTable: "CustomerPhone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
