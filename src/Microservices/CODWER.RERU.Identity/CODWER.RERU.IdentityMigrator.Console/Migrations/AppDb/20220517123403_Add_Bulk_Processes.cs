using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_Bulk_Processes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BulkProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoneProcesses = table.Column<int>(type: "integer", nullable: false),
                    TotalProcesses = table.Column<int>(type: "integer", nullable: false),
                    ProcessType = table.Column<int>(type: "integer", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false),
                    FileId = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BulkProcesses_Processes_ProcessType",
                        column: x => x.ProcessType,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Processes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "BulkAddUsers" },
                    { 2, "BulkAddTests" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BulkProcesses_ProcessType",
                table: "BulkProcesses",
                column: "ProcessType");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_Name",
                table: "Processes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulkProcesses");

            migrationBuilder.DropTable(
                name: "Processes");
        }
    }
}
