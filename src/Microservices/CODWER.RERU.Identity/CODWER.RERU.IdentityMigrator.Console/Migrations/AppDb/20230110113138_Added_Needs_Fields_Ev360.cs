using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Added_Needs_Fields_Ev360 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Need3ProfessionalDevelopmentEvaluated",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Need4ProfessionalDevelopmentEvaluated",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Need5ProfessionalDevelopmentEvaluated",
                table: "Evaluations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Need3ProfessionalDevelopmentEvaluated",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Need4ProfessionalDevelopmentEvaluated",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Need5ProfessionalDevelopmentEvaluated",
                table: "Evaluations");
        }
    }
}
