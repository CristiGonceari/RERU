using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Personal.Data.Persistence.Migrations
{
    public partial class avatar_to_minio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarBase64",
                table: "Avatars",
                newName: "MediaFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediaFileId",
                table: "Avatars",
                newName: "AvatarBase64");
        }
    }
}
