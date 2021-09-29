using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddCaseCategoryToDBContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseCaseCategory_CaseCategory_CategoriesId",
                table: "CaseCaseCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseCategory",
                table: "CaseCategory");

            migrationBuilder.RenameTable(
                name: "CaseCategory",
                newName: "CaseCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseCategories",
                table: "CaseCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseCaseCategory_CaseCategories_CategoriesId",
                table: "CaseCaseCategory",
                column: "CategoriesId",
                principalTable: "CaseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseCaseCategory_CaseCategories_CategoriesId",
                table: "CaseCaseCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseCategories",
                table: "CaseCategories");

            migrationBuilder.RenameTable(
                name: "CaseCategories",
                newName: "CaseCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseCategory",
                table: "CaseCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseCaseCategory_CaseCategory_CategoriesId",
                table: "CaseCaseCategory",
                column: "CategoriesId",
                principalTable: "CaseCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
