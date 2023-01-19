using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Rename_Mai_Functions_to_Employee_Function : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaiFunctions");

            migrationBuilder.CreateTable(
                name: "EmployeeFunctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ColaboratorId = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeFunctions_EvaluationTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "EvaluationTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFunctions_Type",
                table: "EmployeeFunctions",
                column: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeFunctions");

            migrationBuilder.CreateTable(
                name: "MaiFunctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColaboratorId = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaiFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaiFunctions_EvaluationTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "EvaluationTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaiFunctions_Type",
                table: "MaiFunctions",
                column: "Type");
        }
    }
}
