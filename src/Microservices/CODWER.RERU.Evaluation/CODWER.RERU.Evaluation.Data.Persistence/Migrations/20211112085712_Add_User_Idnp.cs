using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Evaluation.Data.Persistence.Migrations
{
    public partial class Add_User_Idnp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Idnp",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "QuestionTypeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "MultipleAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_LocationId",
                table: "Tests",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Locations_LocationId",
                table: "Tests",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Locations_LocationId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_LocationId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Idnp",
                table: "UserProfiles");

            migrationBuilder.UpdateData(
                table: "QuestionTypeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "MultiplyAnswers");
        }
    }
}
