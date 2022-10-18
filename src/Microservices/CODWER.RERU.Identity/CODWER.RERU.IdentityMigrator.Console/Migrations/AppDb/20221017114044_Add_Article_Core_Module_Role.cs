using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_Article_Core_Module_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaFileId",
                table: "CoreArticles",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArticleCoreModuleRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ArticleId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCoreModuleRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleCoreModuleRoles_CoreArticles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "CoreArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleCoreModuleRoles_ModuleRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ModuleRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCoreModuleRoles_ArticleId",
                table: "ArticleCoreModuleRoles",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCoreModuleRoles_RoleId",
                table: "ArticleCoreModuleRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleCoreModuleRoles");

            migrationBuilder.DropColumn(
                name: "MediaFileId",
                table: "CoreArticles");
        }
    }
}
