using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddLocalizationToSubCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SubCategories");

            migrationBuilder.AddColumn<string>(
                name: "Name_Danish",
                table: "SubCategories",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_English",
                table: "SubCategories",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name_Danish",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "Name_English",
                table: "SubCategories");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SubCategories",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }
    }
}
