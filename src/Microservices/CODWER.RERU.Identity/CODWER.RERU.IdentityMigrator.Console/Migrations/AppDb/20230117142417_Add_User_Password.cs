using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_User_Password : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserProfiles",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserProfiles");
        }
    }
}
