using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_SQLServer_Identity_EasyMeal.Migrations
{
    public partial class removedunnecessaryattributesfrommodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCook",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCook",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }
    }
}
