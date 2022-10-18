using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class QUALIFYING_TYPE_NO_QUALIFYING : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "QualifyingTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "NoQualifying" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QualifyingTypeEnum",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
