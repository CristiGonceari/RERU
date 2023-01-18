using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Normalized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FunctionCounterSigner",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FunctionEvaluator",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FunctionSubdivision",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "NameSurnameCounterSigner",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "NameSurnameEvaluated",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "NameSurnameEvaluator",
                table: "Evaluations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FunctionCounterSigner",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FunctionEvaluator",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FunctionSubdivision",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameSurnameCounterSigner",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameSurnameEvaluated",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameSurnameEvaluator",
                table: "Evaluations",
                type: "text",
                nullable: true);
        }
    }
}
