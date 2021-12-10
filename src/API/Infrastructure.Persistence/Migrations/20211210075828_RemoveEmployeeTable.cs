using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class RemoveEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseUpdate_Employees_EmployeeId",
                table: "CaseUpdate");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_CaseUpdate_EmployeeId",
                table: "CaseUpdate");

            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "CaseUpdate");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "CaseUpdate",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "CaseUpdate",
                newName: "EmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "CurrentStatus",
                table: "CaseUpdate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseUpdate_EmployeeId",
                table: "CaseUpdate",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseUpdate_Employees_EmployeeId",
                table: "CaseUpdate",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
