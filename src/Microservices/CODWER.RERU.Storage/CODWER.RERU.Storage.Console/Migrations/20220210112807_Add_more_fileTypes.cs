using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Storage.Console.Migrations
{
    public partial class Add_more_fileTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                value: "Question");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "TestTemplate");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Test");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Media");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Documents");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "IdentityFiles");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Photos");

            migrationBuilder.InsertData(
                table: "FileTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 8, "Request" },
                    { 9, "Order" },
                    { 10, "Cim" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 10);

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

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "media");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "documents");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "identityfiles");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "photos");
        }
    }
}
