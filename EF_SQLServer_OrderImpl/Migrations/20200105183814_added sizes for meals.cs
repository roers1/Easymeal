using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_SQLServer_OrderImpl.Migrations
{
    public partial class addedsizesformeals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "InvoiceLines",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "InvoiceLines");
        }
    }
}
