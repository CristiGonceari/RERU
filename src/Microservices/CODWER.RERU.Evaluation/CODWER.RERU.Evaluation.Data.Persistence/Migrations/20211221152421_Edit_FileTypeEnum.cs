using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Evaluation.Data.Persistence.Migrations
{
    public partial class Edit_FileTypeEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
             table: "FileTypeEnum",
             columns: new[] { "Id", "Name" },
             values: new object[] { 4, "media" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
