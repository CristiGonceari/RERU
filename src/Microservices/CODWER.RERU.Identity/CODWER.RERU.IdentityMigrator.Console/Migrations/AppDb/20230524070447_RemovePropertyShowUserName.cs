using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class RemovePropertyShowUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowUserName",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "ShowUserName",
                table: "EventEvaluators");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowUserName",
                table: "Tests",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowUserName",
                table: "EventEvaluators",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
