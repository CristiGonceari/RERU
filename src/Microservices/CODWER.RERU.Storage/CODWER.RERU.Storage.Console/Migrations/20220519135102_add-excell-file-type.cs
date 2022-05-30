using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Storage.Console.Migrations
{
    public partial class addexcellfiletype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FileTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[] { 11, "procesfile" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
