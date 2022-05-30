using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_Department_Role_To_User_Profile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentColaboratorId",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleColaboratorId",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Roles_ColaboratorId",
                table: "Roles",
                column: "ColaboratorId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Departments_ColaboratorId",
                table: "Departments",
                column: "ColaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_DepartmentColaboratorId",
                table: "UserProfiles",
                column: "DepartmentColaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_RoleColaboratorId",
                table: "UserProfiles",
                column: "RoleColaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Departments_DepartmentColaboratorId",
                table: "UserProfiles",
                column: "DepartmentColaboratorId",
                principalTable: "Departments",
                principalColumn: "ColaboratorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Roles_RoleColaboratorId",
                table: "UserProfiles",
                column: "RoleColaboratorId",
                principalTable: "Roles",
                principalColumn: "ColaboratorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Departments_DepartmentColaboratorId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Roles_RoleColaboratorId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_DepartmentColaboratorId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_RoleColaboratorId",
                table: "UserProfiles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Roles_ColaboratorId",
                table: "Roles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Departments_ColaboratorId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DepartmentColaboratorId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "RoleColaboratorId",
                table: "UserProfiles");
        }
    }
}
