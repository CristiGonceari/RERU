using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Evaluation.Data.Persistence.Migrations
{
    public partial class Add_Score_Formula_For_Tests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormulaForMultipleAnswers",
                table: "TestTemplateSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormulaForOneAnswer",
                table: "TestTemplateSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "NegativeScoreForMultipleAnswers",
                table: "TestTemplateSettings",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NegativeScoreForOneAnswer",
                table: "TestTemplateSettings",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScoreFormulaEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreFormulaEnum", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ScoreFormulaEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "ZeroPercent" },
                    { 1, "OneDivideCountPercent" },
                    { 2, "MinusCorrectOption" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateSettings_FormulaForMultipleAnswers",
                table: "TestTemplateSettings",
                column: "FormulaForMultipleAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateSettings_FormulaForOneAnswer",
                table: "TestTemplateSettings",
                column: "FormulaForOneAnswer");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreFormulaEnum_Name",
                table: "ScoreFormulaEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTemplateSettings_ScoreFormulaEnum_FormulaForMultipleAns~",
                table: "TestTemplateSettings",
                column: "FormulaForMultipleAnswers",
                principalTable: "ScoreFormulaEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTemplateSettings_ScoreFormulaEnum_FormulaForOneAnswer",
                table: "TestTemplateSettings",
                column: "FormulaForOneAnswer",
                principalTable: "ScoreFormulaEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestTemplateSettings_ScoreFormulaEnum_FormulaForMultipleAns~",
                table: "TestTemplateSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTemplateSettings_ScoreFormulaEnum_FormulaForOneAnswer",
                table: "TestTemplateSettings");

            migrationBuilder.DropTable(
                name: "ScoreFormulaEnum");

            migrationBuilder.DropIndex(
                name: "IX_TestTemplateSettings_FormulaForMultipleAnswers",
                table: "TestTemplateSettings");

            migrationBuilder.DropIndex(
                name: "IX_TestTemplateSettings_FormulaForOneAnswer",
                table: "TestTemplateSettings");

            migrationBuilder.DropColumn(
                name: "FormulaForMultipleAnswers",
                table: "TestTemplateSettings");

            migrationBuilder.DropColumn(
                name: "FormulaForOneAnswer",
                table: "TestTemplateSettings");

            migrationBuilder.DropColumn(
                name: "NegativeScoreForMultipleAnswers",
                table: "TestTemplateSettings");

            migrationBuilder.DropColumn(
                name: "NegativeScoreForOneAnswer",
                table: "TestTemplateSettings");
        }
    }
}
