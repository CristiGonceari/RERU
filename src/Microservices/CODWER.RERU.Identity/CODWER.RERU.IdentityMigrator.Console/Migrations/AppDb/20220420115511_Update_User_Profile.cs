using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Update_User_Profile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_CandidatePositions_CandidatePositionId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_CandidatePositionId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CandidatePositionId",
                table: "UserProfiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidatePositionId",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CandidatePositionId",
                table: "UserProfiles",
                column: "CandidatePositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_CandidatePositions_CandidatePositionId",
                table: "UserProfiles",
                column: "CandidatePositionId",
                principalTable: "CandidatePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
