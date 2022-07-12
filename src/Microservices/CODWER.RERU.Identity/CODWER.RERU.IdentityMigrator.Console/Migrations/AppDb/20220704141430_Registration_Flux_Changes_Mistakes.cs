using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Registration_Flux_Changes_Mistakes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialStatuses_UserProfiles_UserProfileid",
                table: "MaterialStatuses");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "MobilePhone",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "UserProfileid",
                table: "MaterialStatuses",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialStatuses_UserProfileid",
                table: "MaterialStatuses",
                newName: "IX_MaterialStatuses_UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialStatuses_UserProfiles_UserProfileId",
                table: "MaterialStatuses",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialStatuses_UserProfiles_UserProfileId",
                table: "MaterialStatuses");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "MaterialStatuses",
                newName: "UserProfileid");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialStatuses_UserProfileId",
                table: "MaterialStatuses",
                newName: "IX_MaterialStatuses_UserProfileid");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "UserProfiles",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MobilePhone",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialStatuses_UserProfiles_UserProfileid",
                table: "MaterialStatuses",
                column: "UserProfileid",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
