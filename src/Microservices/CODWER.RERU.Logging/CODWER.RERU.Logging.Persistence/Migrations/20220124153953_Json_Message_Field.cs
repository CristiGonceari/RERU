using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Logging.Persistence.Migrations
{
    public partial class Json_Message_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JsonMessage",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonMessage",
                table: "Logs");
        }
    }
}
