using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffee.Migrations
{
    public partial class CoffeeCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coffees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Tempurature = table.Column<double>(type: "REAL", nullable: false),
                    Time = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coffees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "Id", "Name", "Tempurature", "Time" },
                values: new object[] { 1, "Капучино", 60.0, 30 });

            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "Id", "Name", "Tempurature", "Time" },
                values: new object[] { 2, "Американо", 70.0, 25 });

            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "Id", "Name", "Tempurature", "Time" },
                values: new object[] { 3, "Раф", 40.0, 22 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coffees");
        }
    }
}
