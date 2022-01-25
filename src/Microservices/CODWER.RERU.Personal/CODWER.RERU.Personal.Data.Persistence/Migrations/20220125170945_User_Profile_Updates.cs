using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Personal.Data.Persistence.Migrations
{
    public partial class User_Profile_Updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserProfiles",
                newName: "Idnp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Idnp",
                table: "UserProfiles",
                newName: "UserId");
        }
    }
}
