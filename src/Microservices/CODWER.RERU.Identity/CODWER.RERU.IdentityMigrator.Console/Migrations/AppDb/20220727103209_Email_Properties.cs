using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Email_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailNotificationProperty_EmailNotifications_EmailNotificat~",
                table: "EmailNotificationProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailNotificationProperty",
                table: "EmailNotificationProperty");

            migrationBuilder.RenameTable(
                name: "EmailNotificationProperty",
                newName: "EmailNotificationProperties");

            migrationBuilder.RenameIndex(
                name: "IX_EmailNotificationProperty_EmailNotificationId",
                table: "EmailNotificationProperties",
                newName: "IX_EmailNotificationProperties_EmailNotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailNotificationProperties",
                table: "EmailNotificationProperties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailNotificationProperties_EmailNotifications_EmailNotific~",
                table: "EmailNotificationProperties",
                column: "EmailNotificationId",
                principalTable: "EmailNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailNotificationProperties_EmailNotifications_EmailNotific~",
                table: "EmailNotificationProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailNotificationProperties",
                table: "EmailNotificationProperties");

            migrationBuilder.RenameTable(
                name: "EmailNotificationProperties",
                newName: "EmailNotificationProperty");

            migrationBuilder.RenameIndex(
                name: "IX_EmailNotificationProperties_EmailNotificationId",
                table: "EmailNotificationProperty",
                newName: "IX_EmailNotificationProperty_EmailNotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailNotificationProperty",
                table: "EmailNotificationProperty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailNotificationProperty_EmailNotifications_EmailNotificat~",
                table: "EmailNotificationProperty",
                column: "EmailNotificationId",
                principalTable: "EmailNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
