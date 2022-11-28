using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class PropertyEvaluations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EvaluationStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "CounterSignAccept" },
                    { 6, "CounterSignReject" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
