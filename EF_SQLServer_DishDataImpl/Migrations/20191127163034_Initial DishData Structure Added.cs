using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_SQLServer_DishDataImpl.Migrations
{
    public partial class InitialDishDataStructureAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    DayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayOfTheWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    DishId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Starter = table.Column<bool>(nullable: false),
                    MainDish = table.Column<bool>(nullable: false),
                    Dessert = table.Column<bool>(nullable: false),
                    Salt = table.Column<bool>(nullable: false),
                    Diabetes = table.Column<bool>(nullable: false),
                    Glutes = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.DishId);
                });

            migrationBuilder.CreateTable(
                name: "Weekplannings",
                columns: table => new
                {
                    WeekplanningId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(nullable: false),
                    Week = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weekplannings", x => x.WeekplanningId);
                });

            migrationBuilder.CreateTable(
                name: "DayDishes",
                columns: table => new
                {
                    DayId = table.Column<int>(nullable: false),
                    DishId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayDishes", x => new { x.DayId, x.DishId });
                    table.ForeignKey(
                        name: "FK_DayDishes_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayDishes_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeekplanningDays",
                columns: table => new
                {
                    WeekplanningId = table.Column<int>(nullable: false),
                    DayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekplanningDays", x => new { x.WeekplanningId, x.DayId });
                    table.ForeignKey(
                        name: "FK_WeekplanningDays_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeekplanningDays_Weekplannings_WeekplanningId",
                        column: x => x.WeekplanningId,
                        principalTable: "Weekplannings",
                        principalColumn: "WeekplanningId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayDishes_DishId",
                table: "DayDishes",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekplanningDays_DayId",
                table: "WeekplanningDays",
                column: "DayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayDishes");

            migrationBuilder.DropTable(
                name: "WeekplanningDays");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Weekplannings");
        }
    }
}
