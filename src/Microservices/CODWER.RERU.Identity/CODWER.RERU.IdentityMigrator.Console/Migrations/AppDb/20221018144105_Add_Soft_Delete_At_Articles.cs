using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_Soft_Delete_At_Articles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "ArticleEvaluationModuleRoles",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ArticleEvaluationModuleRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "ArticleCoreModuleRoles",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ArticleCoreModuleRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "ArticleEvaluationModuleRoles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ArticleEvaluationModuleRoles");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "ArticleCoreModuleRoles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ArticleCoreModuleRoles");
        }
    }
}
