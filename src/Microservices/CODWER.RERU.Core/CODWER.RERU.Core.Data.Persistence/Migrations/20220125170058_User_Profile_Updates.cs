using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Core.Data.Persistence.Migrations
{
    public partial class User_Profile_Updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Idnp",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Idnp",
                table: "UserProfiles");
        }
    }
}
