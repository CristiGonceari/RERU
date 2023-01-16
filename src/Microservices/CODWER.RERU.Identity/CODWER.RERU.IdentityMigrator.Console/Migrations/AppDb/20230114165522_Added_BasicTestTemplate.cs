using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Added_BasicTestTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifierEnum_FinalEvaluationQualification",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationEvaluationObtained2Y~",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationEvaluationObtainedPr~",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationQuarter1",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationQuarter2",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationQuarter3",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationQuarter4",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualifierPartialEvaluations",
                table: "Evaluations");

            migrationBuilder.DropTable(
                name: "QualifierEnum");

            migrationBuilder.AddColumn<int>(
                name: "BasicTestTemplate",
                table: "TestTemplates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BasicTestTemplateEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicTestTemplateEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualifiersEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifiersEnum", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BasicTestTemplateEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "PregatireGenerala" },
                    { 2, "PregatireDeSpecialitate" },
                    { 3, "InstructiaTragerii" },
                    { 4, "InterventiaProfesionala" }
                });

            migrationBuilder.InsertData(
                table: "QualifiersEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Dissatisfied" },
                    { 2, "Satisfied" },
                    { 3, "Good" },
                    { 4, "VeryGood" }
                });

            migrationBuilder.InsertData(
                table: "ServiceDuringEvaluationCourse",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Default" });

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplates_BasicTestTemplate",
                table: "TestTemplates",
                column: "BasicTestTemplate");

            migrationBuilder.CreateIndex(
                name: "IX_BasicTestTemplateEnum_Name",
                table: "BasicTestTemplateEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QualifiersEnum_Name",
                table: "QualifiersEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifiersEnum_FinalEvaluationQualification",
                table: "Evaluations",
                column: "FinalEvaluationQualification",
                principalTable: "QualifiersEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationEvaluationObtained2~",
                table: "Evaluations",
                column: "QualificationEvaluationObtained2YearsPast",
                principalTable: "QualifiersEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationEvaluationObtainedP~",
                table: "Evaluations",
                column: "QualificationEvaluationObtainedPreviousYear",
                principalTable: "QualifiersEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationQuarter1",
                table: "Evaluations",
                column: "QualificationQuarter1",
                principalTable: "QualifiersEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationQuarter2",
                table: "Evaluations",
                column: "QualificationQuarter2",
                principalTable: "QualifiersEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationQuarter3",
                table: "Evaluations",
                column: "QualificationQuarter3",
                principalTable: "QualifiersEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationQuarter4",
                table: "Evaluations",
                column: "QualificationQuarter4",
                principalTable: "QualifiersEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualifierPartialEvaluations",
                table: "Evaluations",
                column: "QualifierPartialEvaluations",
                principalTable: "QualifiersEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTemplates_BasicTestTemplateEnum_BasicTestTemplate",
                table: "TestTemplates",
                column: "BasicTestTemplate",
                principalTable: "BasicTestTemplateEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifiersEnum_FinalEvaluationQualification",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationEvaluationObtained2~",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationEvaluationObtainedP~",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationQuarter1",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationQuarter2",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationQuarter3",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualificationQuarter4",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_QualifiersEnum_QualifierPartialEvaluations",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTemplates_BasicTestTemplateEnum_BasicTestTemplate",
                table: "TestTemplates");

            migrationBuilder.DropTable(
                name: "BasicTestTemplateEnum");

            migrationBuilder.DropTable(
                name: "QualifiersEnum");

            migrationBuilder.DropIndex(
                name: "IX_TestTemplates_BasicTestTemplate",
                table: "TestTemplates");

            migrationBuilder.DeleteData(
                table: "ServiceDuringEvaluationCourse",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "BasicTestTemplate",
                table: "TestTemplates");

            migrationBuilder.CreateTable(
                name: "QualifierEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifierEnum", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "QualifierEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "VeryGood" },
                    { 2, "Good" },
                    { 3, "Satisfied" },
                    { 4, "Dissatisfied" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_QualifierEnum_Name",
                table: "QualifierEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifierEnum_FinalEvaluationQualification",
                table: "Evaluations",
                column: "FinalEvaluationQualification",
                principalTable: "QualifierEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationEvaluationObtained2Y~",
                table: "Evaluations",
                column: "QualificationEvaluationObtained2YearsPast",
                principalTable: "QualifierEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationEvaluationObtainedPr~",
                table: "Evaluations",
                column: "QualificationEvaluationObtainedPreviousYear",
                principalTable: "QualifierEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationQuarter1",
                table: "Evaluations",
                column: "QualificationQuarter1",
                principalTable: "QualifierEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationQuarter2",
                table: "Evaluations",
                column: "QualificationQuarter2",
                principalTable: "QualifierEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationQuarter3",
                table: "Evaluations",
                column: "QualificationQuarter3",
                principalTable: "QualifierEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualificationQuarter4",
                table: "Evaluations",
                column: "QualificationQuarter4",
                principalTable: "QualifierEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_QualifierEnum_QualifierPartialEvaluations",
                table: "Evaluations",
                column: "QualifierPartialEvaluations",
                principalTable: "QualifierEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
