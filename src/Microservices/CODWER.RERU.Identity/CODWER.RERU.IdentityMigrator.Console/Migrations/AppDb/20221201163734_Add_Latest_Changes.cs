using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_Latest_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdministrativeActOfStudies",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdministrativeActService",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "CheckComment1",
                table: "Evaluations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CheckComment2",
                table: "Evaluations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CheckComment3",
                table: "Evaluations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CheckComment4",
                table: "Evaluations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CommentsEvaluated",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentsEvaluator",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAcceptOrRejectEvaluated",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompletionCounterSigner",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompletionGeneralData",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEvaluatiorInterview",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLiftingSanction",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSanctionApplication",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSettingIindividualGoals",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EducationEnum",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "FinalEvaluationQualification",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "FinalScorePartialEvaluations",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FunctionCounterSigner",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FunctionEvaluated",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FunctionEvaluator",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Goal1",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Goal2",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Goal3",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Goal4",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Goal5",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KPI1",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KPI2",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KPI3",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KPI4",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KPI5",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameSurnameCounterSigner",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameSurnameEvaluated",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameSurnameEvaluator",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Need1ProfessionalDevelopmentEvaluated",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Need2ProfessionalDevelopmentEvaluated",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherComments",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PartialEvaluationPeriodFromTo",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PartialEvaluationPeriodUpTo",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PerformanceTerm1",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PerformanceTerm2",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PerformanceTerm3",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PerformanceTerm4",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PerformanceTerm5",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodEvaluatedFromTo",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodEvaluatedUpTo",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodRunningActivityFromTo",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodRunningActivityUpTo",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProfessionalTrainingActivities",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfessionalTrainingActivitiesType",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QualificationEvaluationObtained2YearsPast",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QualificationEvaluationObtainedPreviousYear",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QualificationQuarter1",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QualificationQuarter2",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QualificationQuarter3",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QualificationQuarter4",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QualifierPartialEvaluations",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Question1",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question10",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question11",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question12",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question13",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question2",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question3",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question4",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question5",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question6",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question7",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question8",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Question9",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SanctionAppliedEvaluationCourse",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Score1",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Score2",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Score3",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Score4",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Score5",
                table: "Evaluations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ServiceDuringEvaluationCourse",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SignatureCounterSigner",
                table: "Evaluations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SignatureEvaluated",
                table: "Evaluations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SignatureEvaluator",
                table: "Evaluations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SpecialOrMilitaryGrade",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SpecialOrMilitaryGradeText",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EducationEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalTrainingActivitiesEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalTrainingActivitiesEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalTrainingActivitiesType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalTrainingActivitiesType", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "ServiceDuringEvaluationCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDuringEvaluationCourse", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EducationEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Secondary" },
                    { 2, "Higher" },
                    { 3, "Master" },
                    { 4, "Doctorate" }
                });

            migrationBuilder.InsertData(
                table: "GradeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Special" },
                    { 2, "Military" }
                });

            migrationBuilder.InsertData(
                table: "ProfessionalTrainingActivitiesEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Perfectiosation" },
                    { 2, "Specialization" }
                });

            migrationBuilder.InsertData(
                table: "ProfessionalTrainingActivitiesType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Extern" },
                    { 1, "Intern" }
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

            migrationBuilder.InsertData(
                table: "ServiceDuringEvaluationCourse",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Employment" },
                    { 2, "Transfer" },
                    { 3, "Detachment" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EducationEnum",
                table: "Evaluations",
                column: "EducationEnum");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_FinalEvaluationQualification",
                table: "Evaluations",
                column: "FinalEvaluationQualification");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ProfessionalTrainingActivities",
                table: "Evaluations",
                column: "ProfessionalTrainingActivities");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ProfessionalTrainingActivitiesType",
                table: "Evaluations",
                column: "ProfessionalTrainingActivitiesType");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_QualificationEvaluationObtained2YearsPast",
                table: "Evaluations",
                column: "QualificationEvaluationObtained2YearsPast");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_QualificationEvaluationObtainedPreviousYear",
                table: "Evaluations",
                column: "QualificationEvaluationObtainedPreviousYear");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_QualificationQuarter1",
                table: "Evaluations",
                column: "QualificationQuarter1");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_QualificationQuarter2",
                table: "Evaluations",
                column: "QualificationQuarter2");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_QualificationQuarter3",
                table: "Evaluations",
                column: "QualificationQuarter3");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_QualificationQuarter4",
                table: "Evaluations",
                column: "QualificationQuarter4");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_QualifierPartialEvaluations",
                table: "Evaluations",
                column: "QualifierPartialEvaluations");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ServiceDuringEvaluationCourse",
                table: "Evaluations",
                column: "ServiceDuringEvaluationCourse");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_SpecialOrMilitaryGrade",
                table: "Evaluations",
                column: "SpecialOrMilitaryGrade");

            migrationBuilder.CreateIndex(
                name: "IX_EducationEnum_Name",
                table: "EducationEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GradeEnum_Name",
                table: "GradeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalTrainingActivitiesEnum_Name",
                table: "ProfessionalTrainingActivitiesEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalTrainingActivitiesType_Name",
                table: "ProfessionalTrainingActivitiesType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QualifierEnum_Name",
                table: "QualifierEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDuringEvaluationCourse_Name",
                table: "ServiceDuringEvaluationCourse",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EducationEnum_EducationEnum",
                table: "Evaluations",
                column: "EducationEnum",
                principalTable: "EducationEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_GradeEnum_SpecialOrMilitaryGrade",
                table: "Evaluations",
                column: "SpecialOrMilitaryGrade",
                principalTable: "GradeEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_ProfessionalTrainingActivitiesEnum_Professional~",
                table: "Evaluations",
                column: "ProfessionalTrainingActivities",
                principalTable: "ProfessionalTrainingActivitiesEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_ProfessionalTrainingActivitiesType_Professional~",
                table: "Evaluations",
                column: "ProfessionalTrainingActivitiesType",
                principalTable: "ProfessionalTrainingActivitiesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_ServiceDuringEvaluationCourse_ServiceDuringEval~",
                table: "Evaluations",
                column: "ServiceDuringEvaluationCourse",
                principalTable: "ServiceDuringEvaluationCourse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EducationEnum_EducationEnum",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_GradeEnum_SpecialOrMilitaryGrade",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_ProfessionalTrainingActivitiesEnum_Professional~",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_ProfessionalTrainingActivitiesType_Professional~",
                table: "Evaluations");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_ServiceDuringEvaluationCourse_ServiceDuringEval~",
                table: "Evaluations");

            migrationBuilder.DropTable(
                name: "EducationEnum");

            migrationBuilder.DropTable(
                name: "GradeEnum");

            migrationBuilder.DropTable(
                name: "ProfessionalTrainingActivitiesEnum");

            migrationBuilder.DropTable(
                name: "ProfessionalTrainingActivitiesType");

            migrationBuilder.DropTable(
                name: "QualifierEnum");

            migrationBuilder.DropTable(
                name: "ServiceDuringEvaluationCourse");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_EducationEnum",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_FinalEvaluationQualification",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ProfessionalTrainingActivities",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ProfessionalTrainingActivitiesType",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_QualificationEvaluationObtained2YearsPast",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_QualificationEvaluationObtainedPreviousYear",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_QualificationQuarter1",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_QualificationQuarter2",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_QualificationQuarter3",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_QualificationQuarter4",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_QualifierPartialEvaluations",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ServiceDuringEvaluationCourse",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_SpecialOrMilitaryGrade",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "AdministrativeActOfStudies",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "AdministrativeActService",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "CheckComment1",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "CheckComment2",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "CheckComment3",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "CheckComment4",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "CommentsEvaluated",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "CommentsEvaluator",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DateAcceptOrRejectEvaluated",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DateCompletionCounterSigner",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DateCompletionGeneralData",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DateEvaluatiorInterview",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DateLiftingSanction",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DateSanctionApplication",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DateSettingIindividualGoals",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "EducationEnum",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FinalEvaluationQualification",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FinalScorePartialEvaluations",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FunctionCounterSigner",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FunctionEvaluated",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FunctionEvaluator",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Goal1",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Goal2",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Goal3",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Goal4",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Goal5",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "KPI1",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "KPI2",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "KPI3",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "KPI4",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "KPI5",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "NameSurnameCounterSigner",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "NameSurnameEvaluated",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "NameSurnameEvaluator",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Need1ProfessionalDevelopmentEvaluated",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Need2ProfessionalDevelopmentEvaluated",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "OtherComments",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PartialEvaluationPeriodFromTo",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PartialEvaluationPeriodUpTo",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PerformanceTerm1",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PerformanceTerm2",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PerformanceTerm3",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PerformanceTerm4",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PerformanceTerm5",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PeriodEvaluatedFromTo",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PeriodEvaluatedUpTo",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PeriodRunningActivityFromTo",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PeriodRunningActivityUpTo",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "ProfessionalTrainingActivities",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "ProfessionalTrainingActivitiesType",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "QualificationEvaluationObtained2YearsPast",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "QualificationEvaluationObtainedPreviousYear",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "QualificationQuarter1",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "QualificationQuarter2",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "QualificationQuarter3",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "QualificationQuarter4",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "QualifierPartialEvaluations",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question1",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question10",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question11",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question12",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question13",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question2",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question3",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question4",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question5",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question6",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question7",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question8",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Question9",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SanctionAppliedEvaluationCourse",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Score1",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Score2",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Score3",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Score4",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Score5",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "ServiceDuringEvaluationCourse",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SignatureCounterSigner",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SignatureEvaluated",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SignatureEvaluator",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SpecialOrMilitaryGrade",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SpecialOrMilitaryGradeText",
                table: "Evaluations");
        }
    }
}
