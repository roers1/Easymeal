using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_SQLServer_DishDataImpl.Migrations
{
    public partial class Addedimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Dishes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Dishes");
        }
    }
}
