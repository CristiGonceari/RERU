using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Change_Recruit_To_Disobedient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MilitaryObligationTypeEnum",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "MilitaryObligationTypeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Disobedient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MilitaryObligationTypeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Reservist");

            migrationBuilder.InsertData(
                table: "MilitaryObligationTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Disobedient" });
        }
    }
}
