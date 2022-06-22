using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Change_entity_name_and_fields_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulkProcesses");

            migrationBuilder.DropIndex(
                name: "IX_Processes_Name",
                table: "Processes");

            migrationBuilder.DeleteData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Processes",
                newName: "UpdateById");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Processes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "CreateById",
                table: "Processes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Processes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Processes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Done",
                table: "Processes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FileId",
                table: "Processes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Processes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Processes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProcessesEnumType",
                table: "Processes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Processes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Processes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProcessesEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessesEnum", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProcessesEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "BulkAddUsers" },
                    { 2, "BulkAddTests" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processes_ProcessesEnumType",
                table: "Processes",
                column: "ProcessesEnumType");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesEnum_Name",
                table: "ProcessesEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_ProcessesEnum_ProcessesEnumType",
                table: "Processes",
                column: "ProcessesEnumType",
                principalTable: "ProcessesEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_ProcessesEnum_ProcessesEnumType",
                table: "Processes");

            migrationBuilder.DropTable(
                name: "ProcessesEnum");

            migrationBuilder.DropIndex(
                name: "IX_Processes_ProcessesEnumType",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "Done",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "ProcessesEnumType",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Processes");

            migrationBuilder.RenameColumn(
                name: "UpdateById",
                table: "Processes",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Processes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "BulkProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DoneProcesses = table.Column<int>(type: "integer", nullable: false),
                    FileId = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false),
                    ProcessType = table.Column<int>(type: "integer", nullable: false),
                    TotalProcesses = table.Column<int>(type: "integer", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                name: "IX_Processes_Name",
                table: "Processes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BulkProcesses_ProcessType",
                table: "BulkProcesses",
                column: "ProcessType");
        }
    }
}
