using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class CaseCanOnlyHaveOneCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseCategory");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CategoryId",
                table: "Cases",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Categories_CategoryId",
                table: "Cases",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Categories_CategoryId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_CategoryId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Cases");

            migrationBuilder.CreateTable(
                name: "CaseCategory",
                columns: table => new
                {
                    CasesId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseCategory", x => new { x.CasesId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_CaseCategory_Cases_CasesId",
                        column: x => x.CasesId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseCategory_CategoriesId",
                table: "CaseCategory",
                column: "CategoriesId");
        }
    }
}
