using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Logging.Persistence.Migrations
{
    public partial class First_PostGreSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Project = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    UserIdentifier = table.Column<string>(type: "text", nullable: true),
                    Event = table.Column<string>(type: "text", nullable: true),
                    EventMessage = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    JsonMessage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
