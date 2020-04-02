using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigController.Migrations
{
    public partial class KurosawaConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiConfig",
                columns: table => new
                {
                    Cod = table.Column<uint>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Key = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiConfig", x => x.Cod);
                });

            migrationBuilder.CreateTable(
                name: "BaseConfig",
                columns: table => new
                {
                    Cod = table.Column<uint>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TokenBot = table.Column<string>(nullable: false),
                    Prefix = table.Column<string>(maxLength: 16, nullable: false),
                    IDDono = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseConfig", x => x.Cod);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiConfig");

            migrationBuilder.DropTable(
                name: "BaseConfig");
        }
    }
}
