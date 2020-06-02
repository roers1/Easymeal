using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_SQLServer_OrderImpl.Migrations
{
    public partial class Addeddiscountlogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Discounted",
                table: "Invoices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discounted",
                table: "Invoices");
        }
    }
}
