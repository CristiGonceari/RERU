using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Evaluation.Data.Persistence.Migrations
{
    public partial class TestType_To_TestTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTestTypes_TestTemplates_TestTypeId",
                table: "EventTestTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestTemplates_TestTemplatesId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTemplates_TestTypeSettings_SettingsId",
                table: "TestTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTypeQuestionCategories_TestTemplates_TestTemplatesId",
                table: "TestTypeQuestionCategories");

            migrationBuilder.DropIndex(
                name: "IX_TestTypeQuestionCategories_TestTemplatesId",
                table: "TestTypeQuestionCategories");

            migrationBuilder.DropIndex(
                name: "IX_TestTemplates_SettingsId",
                table: "TestTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TestTemplatesId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestTemplatesId",
                table: "TestTypeQuestionCategories");

            migrationBuilder.DropColumn(
                name: "SettingsId",
                table: "TestTemplates");

            migrationBuilder.DropColumn(
                name: "TestTemplatesId",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "TestTypeId",
                table: "TestTypeSettings",
                newName: "TestTemplateId");

            migrationBuilder.RenameColumn(
                name: "TestTypeId",
                table: "TestTypeQuestionCategories",
                newName: "TestTemplateId");

            migrationBuilder.RenameColumn(
                name: "TestTypeId",
                table: "Tests",
                newName: "TestTemplateId");

            migrationBuilder.RenameColumn(
                name: "TestTypeId",
                table: "EventTestTypes",
                newName: "TestTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_EventTestTypes_TestTypeId",
                table: "EventTestTypes",
                newName: "IX_EventTestTypes_TestTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeSettings_TestTemplateId",
                table: "TestTypeSettings",
                column: "TestTemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeQuestionCategories_TestTemplateId",
                table: "TestTypeQuestionCategories",
                column: "TestTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestTemplateId",
                table: "Tests",
                column: "TestTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventTestTypes_TestTemplates_TestTemplateId",
                table: "EventTestTypes",
                column: "TestTemplateId",
                principalTable: "TestTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestTemplates_TestTemplateId",
                table: "Tests",
                column: "TestTemplateId",
                principalTable: "TestTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTypeQuestionCategories_TestTemplates_TestTemplateId",
                table: "TestTypeQuestionCategories",
                column: "TestTemplateId",
                principalTable: "TestTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTypeSettings_TestTemplates_TestTemplateId",
                table: "TestTypeSettings",
                column: "TestTemplateId",
                principalTable: "TestTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTestTypes_TestTemplates_TestTemplateId",
                table: "EventTestTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestTemplates_TestTemplateId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTypeQuestionCategories_TestTemplates_TestTemplateId",
                table: "TestTypeQuestionCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTypeSettings_TestTemplates_TestTemplateId",
                table: "TestTypeSettings");

            migrationBuilder.DropIndex(
                name: "IX_TestTypeSettings_TestTemplateId",
                table: "TestTypeSettings");

            migrationBuilder.DropIndex(
                name: "IX_TestTypeQuestionCategories_TestTemplateId",
                table: "TestTypeQuestionCategories");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TestTemplateId",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "TestTemplateId",
                table: "TestTypeSettings",
                newName: "TestTypeId");

            migrationBuilder.RenameColumn(
                name: "TestTemplateId",
                table: "TestTypeQuestionCategories",
                newName: "TestTypeId");

            migrationBuilder.RenameColumn(
                name: "TestTemplateId",
                table: "Tests",
                newName: "TestTypeId");

            migrationBuilder.RenameColumn(
                name: "TestTemplateId",
                table: "EventTestTypes",
                newName: "TestTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_EventTestTypes_TestTemplateId",
                table: "EventTestTypes",
                newName: "IX_EventTestTypes_TestTypeId");

            migrationBuilder.AddColumn<int>(
                name: "TestTemplatesId",
                table: "TestTypeQuestionCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SettingsId",
                table: "TestTemplates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestTemplatesId",
                table: "Tests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeQuestionCategories_TestTemplatesId",
                table: "TestTypeQuestionCategories",
                column: "TestTemplatesId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplates_SettingsId",
                table: "TestTemplates",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestTemplatesId",
                table: "Tests",
                column: "TestTemplatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventTestTypes_TestTemplates_TestTypeId",
                table: "EventTestTypes",
                column: "TestTypeId",
                principalTable: "TestTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestTemplates_TestTemplatesId",
                table: "Tests",
                column: "TestTemplatesId",
                principalTable: "TestTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTemplates_TestTypeSettings_SettingsId",
                table: "TestTemplates",
                column: "SettingsId",
                principalTable: "TestTypeSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTypeQuestionCategories_TestTemplates_TestTemplatesId",
                table: "TestTypeQuestionCategories",
                column: "TestTemplatesId",
                principalTable: "TestTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
