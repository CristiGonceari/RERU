using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Undo_MilitaryObligationEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MilitaryObligationTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Reservist" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MilitaryObligationTypeEnum",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
