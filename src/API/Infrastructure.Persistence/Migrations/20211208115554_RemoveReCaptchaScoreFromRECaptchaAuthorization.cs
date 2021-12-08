using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class RemoveReCaptchaScoreFromRECaptchaAuthorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QueueKeys",
                table: "QueueKeys");

            migrationBuilder.DropColumn(
                name: "CaptchaScore",
                table: "QueueKeys");

            migrationBuilder.RenameTable(
                name: "QueueKeys",
                newName: "PreAuthorizations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreAuthorizations",
                table: "PreAuthorizations",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PreAuthorizations",
                table: "PreAuthorizations");

            migrationBuilder.RenameTable(
                name: "PreAuthorizations",
                newName: "QueueKeys");

            migrationBuilder.AddColumn<float>(
                name: "CaptchaScore",
                table: "QueueKeys",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueueKeys",
                table: "QueueKeys",
                column: "DeviceId");
        }
    }
}
