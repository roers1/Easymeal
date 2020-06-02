using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_SQLServer_Identity_EasyMeal.Migrations
{
    public partial class changednamingscheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cook_UserCook",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Customer_UserCustomer",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserCook",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserCustomer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCook",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCustomer",
                table: "AspNetUsers");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cook_AspNetUsers_Email",
                table: "Cook");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_Email",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "UserCook",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCustomer",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserCook",
                table: "AspNetUsers",
                column: "UserCook",
                unique: true,
                filter: "[UserCook] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserCustomer",
                table: "AspNetUsers",
                column: "UserCustomer",
                unique: true,
                filter: "[UserCustomer] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cook_UserCook",
                table: "AspNetUsers",
                column: "UserCook",
                principalTable: "Cook",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Customer_UserCustomer",
                table: "AspNetUsers",
                column: "UserCustomer",
                principalTable: "Customer",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
