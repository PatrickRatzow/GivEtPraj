using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class ConvertCaseCoordsToValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Cases",
                newName: "Coords_Longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Cases",
                newName: "Coords_Latitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coords_Longitude",
                table: "Cases",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "Coords_Latitude",
                table: "Cases",
                newName: "Latitude");
        }
    }
}
