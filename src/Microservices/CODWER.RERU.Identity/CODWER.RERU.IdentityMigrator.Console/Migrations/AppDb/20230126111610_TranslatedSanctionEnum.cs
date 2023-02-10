using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class TranslatedSanctionEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Without");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Warning");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Reprimand");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "HarshRebuke");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Downgrade");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Demotion");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Dismissal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Nu_sunt");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Avertisment");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Mustrare");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Mustrare_aspră");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Retrogradare_cu_un_grad_special");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Retrogradare_în_funcţie");

            migrationBuilder.UpdateData(
                table: "SanctionEnum",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Concediere_din_funcţia_publică_cu_statut_special");
        }
    }
}
