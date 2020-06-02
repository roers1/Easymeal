using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_SQLServer_Identity_EasyMeal.Migrations
{
    public partial class Addeddbsets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cook_AspNetUsers_Email",
                table: "Cook");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_Email",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cook",
                table: "Cook");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Cook",
                newName: "Cooks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cooks",
                table: "Cooks",
                column: "Email");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cooks_AspNetUsers_Email",
                table: "Cooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_Email",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cooks",
                table: "Cooks");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "Cooks",
                newName: "Cook");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cook",
                table: "Cook",
                column: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Cook_AspNetUsers_Email",
                table: "Cook",
                column: "Email",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_AspNetUsers_Email",
                table: "Customer",
                column: "Email",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
