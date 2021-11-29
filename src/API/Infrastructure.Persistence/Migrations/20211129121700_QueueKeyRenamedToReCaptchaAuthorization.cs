using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class QueueKeyRenamedToReCaptchaAuthorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QueueKeys",
                table: "QueueKeys");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QueueKeys");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "QueueKeys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueueKeys",
                table: "QueueKeys",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QueueKeys",
                table: "QueueKeys");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "QueueKeys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "QueueKeys",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueueKeys",
                table: "QueueKeys",
                column: "Id");
        }
    }
}
