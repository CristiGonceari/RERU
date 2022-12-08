using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_PartialEvaluationScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartialEvlauationScore",
                table: "Evaluations",
                newName: "PartialEvaluationScore");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartialEvaluationScore",
                table: "Evaluations",
                newName: "PartialEvlauationScore");
        }
    }
}
