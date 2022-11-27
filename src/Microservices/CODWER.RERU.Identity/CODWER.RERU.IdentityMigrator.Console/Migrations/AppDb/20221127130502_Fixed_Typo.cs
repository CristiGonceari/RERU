using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Fixed_Typo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_UserProfiles_CounterSingerUserProfileId",
                table: "Evaluations");

            migrationBuilder.RenameColumn(
                name: "CounterSingerUserProfileId",
                table: "Evaluations",
                newName: "CounterSignerUserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Evaluations_CounterSingerUserProfileId",
                table: "Evaluations",
                newName: "IX_Evaluations_CounterSignerUserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_UserProfiles_CounterSignerUserProfileId",
                table: "Evaluations",
                column: "CounterSignerUserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_UserProfiles_CounterSignerUserProfileId",
                table: "Evaluations");

            migrationBuilder.RenameColumn(
                name: "CounterSignerUserProfileId",
                table: "Evaluations",
                newName: "CounterSingerUserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Evaluations_CounterSignerUserProfileId",
                table: "Evaluations",
                newName: "IX_Evaluations_CounterSingerUserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_UserProfiles_CounterSingerUserProfileId",
                table: "Evaluations",
                column: "CounterSingerUserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
