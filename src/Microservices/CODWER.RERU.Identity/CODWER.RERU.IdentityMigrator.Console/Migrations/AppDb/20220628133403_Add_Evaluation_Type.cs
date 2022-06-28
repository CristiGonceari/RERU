using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_Evaluation_Type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TestTemplateModeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Evaluation" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TestTemplateModeEnum",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
