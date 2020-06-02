using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_SQLServer_DishDataImpl.Migrations
{
    public partial class Removedactivefieldfromdishmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Dishes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Dishes",
                nullable: false,
                defaultValue: false);
        }
    }
}
