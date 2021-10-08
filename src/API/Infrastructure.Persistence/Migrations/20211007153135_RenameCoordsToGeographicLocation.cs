using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class RenameCoordsToGeographicLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coords_Longitude",
                table: "Cases",
                newName: "GeographicLocation_Longitude");

            migrationBuilder.RenameColumn(
                name: "Coords_Latitude",
                table: "Cases",
                newName: "GeographicLocation_Latitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GeographicLocation_Longitude",
                table: "Cases",
                newName: "Coords_Longitude");

            migrationBuilder.RenameColumn(
                name: "GeographicLocation_Latitude",
                table: "Cases",
                newName: "Coords_Latitude");
        }
    }
}
