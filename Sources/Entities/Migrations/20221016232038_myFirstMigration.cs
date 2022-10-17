using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class myFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "ID", "Image", "Name" },
                values: new object[] { 1, "imageMickael.png", "Mickael" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "ID", "Image", "Name" },
                values: new object[] { 2, "imageJeremy.png", "Jeremy" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "ID", "Image", "Name" },
                values: new object[] { 3, "imageLucas.png", "Lucas" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
