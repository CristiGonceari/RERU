using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_Access_Mode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessModeEnum",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccessModeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessModeEnum", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AccessModeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "OnlyCandidates" },
                    { 1, "CurrentDepartment" },
                    { 2, "AllDepartments" },
                    { 3, "All" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AccessModeEnum",
                table: "UserProfiles",
                column: "AccessModeEnum");

            migrationBuilder.CreateIndex(
                name: "IX_AccessModeEnum_Name",
                table: "AccessModeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AccessModeEnum_AccessModeEnum",
                table: "UserProfiles",
                column: "AccessModeEnum",
                principalTable: "AccessModeEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AccessModeEnum_AccessModeEnum",
                table: "UserProfiles");

            migrationBuilder.DropTable(
                name: "AccessModeEnum");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_AccessModeEnum",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "AccessModeEnum",
                table: "UserProfiles");
        }
    }
}
