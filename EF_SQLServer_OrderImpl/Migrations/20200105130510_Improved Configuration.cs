using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_SQLServer_OrderImpl.Migrations
{
    public partial class ImprovedConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "DayNr",
                table: "InvoiceLines");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Invoices",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "Customer",
                table: "Invoices",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Invoices",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "InvoiceLines",
                newName: "InvoiceLineId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "InvoiceLines",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId",
                table: "InvoiceLines",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "InvoiceLines");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Invoices",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Invoices",
                newName: "Customer");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "Invoices",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "InvoiceLineId",
                table: "InvoiceLines",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "DayNr",
                table: "InvoiceLines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId",
                table: "InvoiceLines",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
