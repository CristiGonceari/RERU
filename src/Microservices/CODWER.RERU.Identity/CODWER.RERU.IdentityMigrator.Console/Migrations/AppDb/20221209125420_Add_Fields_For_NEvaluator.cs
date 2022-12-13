using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_Fields_For_NEvaluator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinalAccumulatedPercentage",
                table: "Tests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalStatusResult",
                table: "Tests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_FinalStatusResult",
                table: "Tests",
                column: "FinalStatusResult");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestResultStatusEnum_FinalStatusResult",
                table: "Tests",
                column: "FinalStatusResult",
                principalTable: "TestResultStatusEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestResultStatusEnum_FinalStatusResult",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_FinalStatusResult",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "FinalAccumulatedPercentage",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "FinalStatusResult",
                table: "Tests");
        }
    }
}
