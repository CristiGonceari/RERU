using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Added_FunctionColaboratorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FunctionColaboratorId",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EmployeeFunctions_ColaboratorId",
                table: "EmployeeFunctions",
                column: "ColaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_FunctionColaboratorId",
                table: "UserProfiles",
                column: "FunctionColaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_EmployeeFunctions_FunctionColaboratorId",
                table: "UserProfiles",
                column: "FunctionColaboratorId",
                principalTable: "EmployeeFunctions",
                principalColumn: "ColaboratorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_EmployeeFunctions_FunctionColaboratorId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_FunctionColaboratorId",
                table: "UserProfiles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EmployeeFunctions_ColaboratorId",
                table: "EmployeeFunctions");

            migrationBuilder.DropColumn(
                name: "FunctionColaboratorId",
                table: "UserProfiles");
        }
    }
}
