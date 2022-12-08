using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_Can_Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "canAccept",
                table: "Evaluations",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "canCounterSign",
                table: "Evaluations",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "canDelete",
                table: "Evaluations",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "canEvaluate",
                table: "Evaluations",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "canFinished",
                table: "Evaluations",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "canAccept",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "canCounterSign",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "canDelete",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "canEvaluate",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "canFinished",
                table: "Evaluations");
        }
    }
}
