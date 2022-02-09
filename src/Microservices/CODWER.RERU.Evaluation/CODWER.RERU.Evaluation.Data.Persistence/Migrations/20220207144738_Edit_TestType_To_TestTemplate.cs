using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Evaluation.Data.Persistence.Migrations
{
    public partial class Edit_TestType_To_TestTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTestTypes_TestTypes_TestTypeId",
                table: "EventTestTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestTypes_TestTypeId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTypeQuestionCategories_TestTypes_TestTypeId",
                table: "TestTypeQuestionCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTypeSettings_TestTypes_TestTypeId",
                table: "TestTypeSettings");

            migrationBuilder.DropTable(
                name: "TestTypes");

            migrationBuilder.DropIndex(
                name: "IX_TestTypeSettings_TestTypeId",
                table: "TestTypeSettings");

            migrationBuilder.DropIndex(
                name: "IX_TestTypeQuestionCategories_TestTypeId",
                table: "TestTypeQuestionCategories");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TestTypeId",
                table: "Tests");

            migrationBuilder.AddColumn<int>(
                name: "TestTemplatesId",
                table: "TestTypeQuestionCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestTemplatesId",
                table: "Tests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TestTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionCount = table.Column<int>(type: "int", nullable: false),
                    MinPercent = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    CategoriesSequence = table.Column<int>(type: "int", nullable: false),
                    SettingsId = table.Column<int>(type: "int", nullable: true),
                    PdfFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTemplates_SequenceEnum_CategoriesSequence",
                        column: x => x.CategoriesSequence,
                        principalTable: "SequenceEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTemplates_TestTypeModeEnum_Mode",
                        column: x => x.Mode,
                        principalTable: "TestTypeModeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTemplates_TestTypeSettings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "TestTypeSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTemplates_TestTypeStatusEnum_Status",
                        column: x => x.Status,
                        principalTable: "TestTypeStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeQuestionCategories_TestTemplatesId",
                table: "TestTypeQuestionCategories",
                column: "TestTemplatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestTemplatesId",
                table: "Tests",
                column: "TestTemplatesId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplates_CategoriesSequence",
                table: "TestTemplates",
                column: "CategoriesSequence");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplates_Mode",
                table: "TestTemplates",
                column: "Mode");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplates_SettingsId",
                table: "TestTemplates",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplates_Status",
                table: "TestTemplates",
                column: "Status");

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
                name: "FK_TestTypeQuestionCategories_TestTemplates_TestTemplatesId",
                table: "TestTypeQuestionCategories",
                column: "TestTemplatesId",
                principalTable: "TestTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTestTypes_TestTemplates_TestTypeId",
                table: "EventTestTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestTemplates_TestTemplatesId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTypeQuestionCategories_TestTemplates_TestTemplatesId",
                table: "TestTypeQuestionCategories");

            migrationBuilder.DropTable(
                name: "TestTemplates");

            migrationBuilder.DropIndex(
                name: "IX_TestTypeQuestionCategories_TestTemplatesId",
                table: "TestTypeQuestionCategories");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TestTemplatesId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestTemplatesId",
                table: "TestTypeQuestionCategories");

            migrationBuilder.DropColumn(
                name: "TestTemplatesId",
                table: "Tests");

            migrationBuilder.CreateTable(
                name: "TestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriesSequence = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    MinPercent = table.Column<int>(type: "int", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PdfFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionCount = table.Column<int>(type: "int", nullable: false),
                    Rules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTypes_SequenceEnum_CategoriesSequence",
                        column: x => x.CategoriesSequence,
                        principalTable: "SequenceEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTypes_TestTypeModeEnum_Mode",
                        column: x => x.Mode,
                        principalTable: "TestTypeModeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTypes_TestTypeStatusEnum_Status",
                        column: x => x.Status,
                        principalTable: "TestTypeStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeSettings_TestTypeId",
                table: "TestTypeSettings",
                column: "TestTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeQuestionCategories_TestTypeId",
                table: "TestTypeQuestionCategories",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestTypeId",
                table: "Tests",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypes_CategoriesSequence",
                table: "TestTypes",
                column: "CategoriesSequence");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypes_Mode",
                table: "TestTypes",
                column: "Mode");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypes_Status",
                table: "TestTypes",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_EventTestTypes_TestTypes_TestTypeId",
                table: "EventTestTypes",
                column: "TestTypeId",
                principalTable: "TestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestTypes_TestTypeId",
                table: "Tests",
                column: "TestTypeId",
                principalTable: "TestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTypeQuestionCategories_TestTypes_TestTypeId",
                table: "TestTypeQuestionCategories",
                column: "TestTypeId",
                principalTable: "TestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTypeSettings_TestTypes_TestTypeId",
                table: "TestTypeSettings",
                column: "TestTypeId",
                principalTable: "TestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
