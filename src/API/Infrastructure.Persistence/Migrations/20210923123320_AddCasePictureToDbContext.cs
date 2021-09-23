using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddCasePictureToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CasePicture_Cases_CaseId",
                table: "CasePicture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CasePicture",
                table: "CasePicture");

            migrationBuilder.RenameTable(
                name: "CasePicture",
                newName: "CasePictures");

            migrationBuilder.RenameIndex(
                name: "IX_CasePicture_CaseId",
                table: "CasePictures",
                newName: "IX_CasePictures_CaseId");

            migrationBuilder.AlterColumn<int>(
                name: "CaseId",
                table: "CasePictures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CasePictures",
                table: "CasePictures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CasePictures_Cases_CaseId",
                table: "CasePictures",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CasePictures_Cases_CaseId",
                table: "CasePictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CasePictures",
                table: "CasePictures");

            migrationBuilder.RenameTable(
                name: "CasePictures",
                newName: "CasePicture");

            migrationBuilder.RenameIndex(
                name: "IX_CasePictures_CaseId",
                table: "CasePicture",
                newName: "IX_CasePicture_CaseId");

            migrationBuilder.AlterColumn<int>(
                name: "CaseId",
                table: "CasePicture",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CasePicture",
                table: "CasePicture",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CasePicture_Cases_CaseId",
                table: "CasePicture",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id");
        }
    }
}
