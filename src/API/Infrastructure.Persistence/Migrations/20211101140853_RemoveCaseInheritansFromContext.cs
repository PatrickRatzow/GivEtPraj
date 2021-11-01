using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class RemoveCaseInheritansFromContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseCases_Categories_CategoryId",
                table: "BaseCases");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseUpdate_BaseCases_CaseId",
                table: "CaseUpdate");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_BaseCases_CaseId",
                table: "Pictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseCases",
                table: "BaseCases");

            migrationBuilder.RenameTable(
                name: "BaseCases",
                newName: "Cases");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCases_CategoryId",
                table: "Cases",
                newName: "IX_Cases_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cases",
                table: "Cases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Categories_CategoryId",
                table: "Cases",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseUpdate_Cases_CaseId",
                table: "CaseUpdate",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Cases_CaseId",
                table: "Pictures",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Categories_CategoryId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseUpdate_Cases_CaseId",
                table: "CaseUpdate");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Cases_CaseId",
                table: "Pictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cases",
                table: "Cases");

            migrationBuilder.RenameTable(
                name: "Cases",
                newName: "BaseCases");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_CategoryId",
                table: "BaseCases",
                newName: "IX_BaseCases_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseCases",
                table: "BaseCases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCases_Categories_CategoryId",
                table: "BaseCases",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseUpdate_BaseCases_CaseId",
                table: "CaseUpdate",
                column: "CaseId",
                principalTable: "BaseCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_BaseCases_CaseId",
                table: "Pictures",
                column: "CaseId",
                principalTable: "BaseCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
