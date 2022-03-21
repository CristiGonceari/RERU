using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Core.Data.Persistence.Migrations
{
    public partial class avatar_to_minio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Documents_AvatarId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_AvatarId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "MediaFileId",
                table: "UserProfiles",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaFileId",
                table: "UserProfiles");

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AvatarId",
                table: "UserProfiles",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Documents_AvatarId",
                table: "UserProfiles",
                column: "AvatarId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
