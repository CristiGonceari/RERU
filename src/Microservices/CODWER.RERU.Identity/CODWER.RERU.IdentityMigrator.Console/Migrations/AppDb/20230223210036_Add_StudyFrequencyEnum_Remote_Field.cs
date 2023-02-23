using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_StudyFrequencyEnum_Remote_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StudyFrequencyEnum",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Remote" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudyFrequencyEnum",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
