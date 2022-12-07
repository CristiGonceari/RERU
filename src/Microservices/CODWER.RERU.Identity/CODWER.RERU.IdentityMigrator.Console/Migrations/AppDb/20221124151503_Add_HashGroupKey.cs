using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_HashGroupKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashGroupKey",
                table: "Tests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashGroupKey",
                table: "TestQuestions",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashGroupKey",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "HashGroupKey",
                table: "TestQuestions");
        }
    }
}
