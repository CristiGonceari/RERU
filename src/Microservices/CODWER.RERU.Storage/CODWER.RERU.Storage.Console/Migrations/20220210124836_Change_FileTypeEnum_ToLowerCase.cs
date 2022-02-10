using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Storage.Console.Migrations
{
    public partial class Change_FileTypeEnum_ToLowerCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "request");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "order");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "cim");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Request");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Order");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Cim");
        }
    }
}
