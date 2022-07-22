using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Email_Cron_Job : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailNotifications_EmailType_EmailType",
                table: "EmailNotifications");

            migrationBuilder.DropTable(
                name: "EmailType");

            migrationBuilder.DropIndex(
                name: "IX_EmailNotifications_EmailType",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "EmailType",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "EmailNotifications");

            migrationBuilder.RenameColumn(
                name: "UpdateById",
                table: "EmailNotifications",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "EmailNotifications",
                newName: "InUpdateProcess");

            migrationBuilder.RenameColumn(
                name: "CreateById",
                table: "EmailNotifications",
                newName: "Subject");

            migrationBuilder.AddColumn<string>(
                name: "HtmlTemplateAddress",
                table: "EmailNotifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "EmailNotifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "EmailNotifications",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "EmailNotificationProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KeyToReplace = table.Column<string>(type: "text", nullable: true),
                    ValueToReplace = table.Column<string>(type: "text", nullable: true),
                    EmailNotificationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotificationProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailNotificationProperty_EmailNotifications_EmailNotificat~",
                        column: x => x.EmailNotificationId,
                        principalTable: "EmailNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotificationProperty_EmailNotificationId",
                table: "EmailNotificationProperty",
                column: "EmailNotificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailNotificationProperty");

            migrationBuilder.DropColumn(
                name: "HtmlTemplateAddress",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "EmailNotifications");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "EmailNotifications");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "EmailNotifications",
                newName: "UpdateById");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "EmailNotifications",
                newName: "CreateById");

            migrationBuilder.RenameColumn(
                name: "InUpdateProcess",
                table: "EmailNotifications",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "EmailNotifications",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "EmailNotifications",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmailType",
                table: "EmailNotifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "EmailNotifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "EmailNotifications",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmailType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmailType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "AddTestCandidateEmail" },
                    { 2, "AddTestEvaluatorEmail" },
                    { 3, "AssignUserToEvent" },
                    { 4, "AssignEvaluatorToEvent" },
                    { 5, "AssignResponsiblePersonToEvent" },
                    { 6, "AddUser" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotifications_EmailType",
                table: "EmailNotifications",
                column: "EmailType");

            migrationBuilder.CreateIndex(
                name: "IX_EmailType_Name",
                table: "EmailType",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailNotifications_EmailType_EmailType",
                table: "EmailNotifications",
                column: "EmailType",
                principalTable: "EmailType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
