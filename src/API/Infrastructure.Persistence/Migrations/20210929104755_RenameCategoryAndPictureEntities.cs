using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class RenameCategoryAndPictureEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseCaseCategory");

            migrationBuilder.DropTable(
                name: "CasePictures");

            migrationBuilder.DropTable(
                name: "CaseCategories");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_CaseId",
                table: "Pictures",
                column: "CaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseCategory");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.CreateTable(
                name: "CaseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CasePictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasePictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasePictures_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseCaseCategory",
                columns: table => new
                {
                    CasesId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseCaseCategory", x => new { x.CasesId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_CaseCaseCategory_CaseCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "CaseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseCaseCategory_Cases_CasesId",
                        column: x => x.CasesId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseCaseCategory_CategoriesId",
                table: "CaseCaseCategory",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_CasePictures_CaseId",
                table: "CasePictures",
                column: "CaseId");
        }
    }
}
