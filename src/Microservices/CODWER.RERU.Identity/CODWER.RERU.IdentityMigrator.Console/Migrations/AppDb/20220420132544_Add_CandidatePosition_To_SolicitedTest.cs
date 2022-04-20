using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_CandidatePosition_To_SolicitedTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidatePositionId",
                table: "SolicitedTests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedTests_CandidatePositionId",
                table: "SolicitedTests",
                column: "CandidatePositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitedTests_CandidatePositions_CandidatePositionId",
                table: "SolicitedTests",
                column: "CandidatePositionId",
                principalTable: "CandidatePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitedTests_CandidatePositions_CandidatePositionId",
                table: "SolicitedTests");

            migrationBuilder.DropIndex(
                name: "IX_SolicitedTests_CandidatePositionId",
                table: "SolicitedTests");

            migrationBuilder.DropColumn(
                name: "CandidatePositionId",
                table: "SolicitedTests");
        }
    }
}
