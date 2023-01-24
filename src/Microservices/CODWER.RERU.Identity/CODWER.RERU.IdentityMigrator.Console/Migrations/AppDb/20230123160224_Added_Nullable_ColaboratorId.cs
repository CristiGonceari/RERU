using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Added_Nullable_ColaboratorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_EmployeeFunctions_FunctionColaboratorId",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "ColaboratorId",
                table: "EmployeeFunctions",
                type: "integer",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EmployeeFunctions_ColaboratorId",
                table: "EmployeeFunctions",
                column: "ColaboratorId");

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

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EmployeeFunctions_ColaboratorId",
                table: "EmployeeFunctions");

            migrationBuilder.AlterColumn<int>(
                name: "ColaboratorId",
                table: "EmployeeFunctions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_EmployeeFunctions_FunctionColaboratorId",
                table: "UserProfiles",
                column: "FunctionColaboratorId",
                principalTable: "EmployeeFunctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
