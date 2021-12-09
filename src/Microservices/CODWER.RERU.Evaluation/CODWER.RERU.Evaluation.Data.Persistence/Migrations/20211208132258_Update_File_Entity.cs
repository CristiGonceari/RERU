using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Evaluation.Data.Persistence.Migrations
{
    public partial class Update_File_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 0);

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UpdateById",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Files");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "question");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "testtemplate");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "test");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<string>(
                name: "CreateById",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Files",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Files",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Files",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdateById",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Files",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TestTemplate");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Test");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Media");

            migrationBuilder.InsertData(
                table: "FileTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[] { 0, "Question" });
        }
    }
}
