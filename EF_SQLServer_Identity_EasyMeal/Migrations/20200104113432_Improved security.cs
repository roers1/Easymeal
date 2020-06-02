using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_SQLServer_Identity_EasyMeal.Migrations
{
    public partial class Improvedsecurity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cooks_AspNetUsers_Email",
                table: "Cooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_Email",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Cooks",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cooks_AspNetUsers_Id",
                table: "Cooks",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_Id",
                table: "Customers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cooks_AspNetUsers_Id",
                table: "Cooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_Id",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cooks",
                newName: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Cooks_AspNetUsers_Email",
                table: "Cooks",
                column: "Email",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_Email",
                table: "Customers",
                column: "Email",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
