using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class QUALIFYING_TYPE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QualifyingType",
                table: "TestTemplates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotRecommendedFor",
                table: "Tests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecommendedFor",
                table: "Tests",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QualifyingTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifyingTypeEnum", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "QualifyingTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "PassedNotPassed" },
                    { 2, "AbleNotAble" },
                    { 3, "AcceptedRejected" },
                    { 4, "Recommended" }
                });

            migrationBuilder.InsertData(
                table: "TestResultStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Able" },
                    { 4, "NotAble" },
                    { 5, "Accepted" },
                    { 6, "Rejected" },
                    { 7, "Recommended" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplates_QualifyingType",
                table: "TestTemplates",
                column: "QualifyingType");

            migrationBuilder.CreateIndex(
                name: "IX_QualifyingTypeEnum_Name",
                table: "QualifyingTypeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTemplates_QualifyingTypeEnum_QualifyingType",
                table: "TestTemplates",
                column: "QualifyingType",
                principalTable: "QualifyingTypeEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestTemplates_QualifyingTypeEnum_QualifyingType",
                table: "TestTemplates");

            migrationBuilder.DropTable(
                name: "QualifyingTypeEnum");

            migrationBuilder.DropIndex(
                name: "IX_TestTemplates_QualifyingType",
                table: "TestTemplates");

            migrationBuilder.DeleteData(
                table: "TestResultStatusEnum",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TestResultStatusEnum",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TestResultStatusEnum",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TestResultStatusEnum",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TestResultStatusEnum",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "QualifyingType",
                table: "TestTemplates");

            migrationBuilder.DropColumn(
                name: "NotRecommendedFor",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "RecommendedFor",
                table: "Tests");
        }
    }
}
