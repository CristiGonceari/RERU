using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Created_Email_Record_Time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "EmailNotifications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "EmailNotifications");
        }
    }
}
