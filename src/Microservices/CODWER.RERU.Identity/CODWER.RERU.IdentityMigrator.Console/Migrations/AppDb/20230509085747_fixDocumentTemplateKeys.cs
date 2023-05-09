using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class fixDocumentTemplateKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"DocumentTemplateKeys\" SET \"FileType\" = 3 WHERE \"DocumentTemplateKeys\".\"FileType\" = 2");
            migrationBuilder.Sql("UPDATE \"DocumentTemplateKeys\" SET \"FileType\" = 2 WHERE \"DocumentTemplateKeys\".\"FileType\" = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"DocumentTemplateKeys\" SET \"FileType\" = 1 WHERE \"DocumentTemplateKeys\".\"FileType\" = 2");
            migrationBuilder.Sql("UPDATE \"DocumentTemplateKeys\" SET \"FileType\" = 2 WHERE \"DocumentTemplateKeys\".\"FileType\" = 3");
        }
    }
}
