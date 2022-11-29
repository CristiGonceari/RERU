using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class MoreEvaluationProperty : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "AppointmentDate",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckComment1",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckComment2",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckComment3",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckComment4",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentsEvaluatedEmployee",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateAgreementDisagreementAwardedQualifier",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateAppreciationLevelMnifestationEvaluatedCriteria",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateCompletionCountersignatory",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompletionGeneralData",
                table: "Evaluations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DateEvaluatiorInterview",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateLiftingSanction",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateSanctionApplication",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateSettingIindividualGoals",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateSignatureEvaluatedEmployee",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DecisionCountersignatory",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EvaluatorComments",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinalEvaluationQualification",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinalScorePartialEvaluations",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Function",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FunctionSubdivision",
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
                name: "ModificationServiceReportDuringEvaluationCourse",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameSurnameEvaluatedEmployee",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameSurnameFunctionCountersignatory",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameSurnameFunctionEvaluator",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Need1ProfessionalDevelopmentEvaluatedEmployee",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Need2ProfessionalDevelopmentEvaluatedEmployee",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherComments",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartialEvaluationPeriod",
                table: "Evaluations",
                type: "text",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "PeriodEvaluatedFromTo",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PeriodEvaluatedUpTo",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PeriodRunningActivity",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalTrainingActivities",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualificationEvaluationObtained2YearsPast",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualificationEvaluationObtainedPreviousYear",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualificationQuarter1",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualificationQuarter2",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualificationQuarter3",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualificationQuarter4",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualifierPartialEvaluations",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question1",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question10",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question11",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question12",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question13",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question2",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question3",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question4",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question5",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question6",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question7",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question8",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question9",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SanctionAppliedEvaluationCourse",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Score1",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Score2",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Score3",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Score4",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Score5",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialOrMilitaryGrade",
                table: "Evaluations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "CommentsEvaluatedEmployee",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DateAgreementDisagreementAwardedQualifier",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DateAppreciationLevelMnifestationEvaluatedCriteria",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DateCompletionCountersignatory",
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
                name: "DateSignatureEvaluatedEmployee",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "DecisionCountersignatory",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "EvaluatorComments",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FinalEvaluationQualification",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FinalScorePartialEvaluations",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Function",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FunctionSubdivision",
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
                name: "ModificationServiceReportDuringEvaluationCourse",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "NameSurnameEvaluatedEmployee",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "NameSurnameFunctionCountersignatory",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "NameSurnameFunctionEvaluator",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Need1ProfessionalDevelopmentEvaluatedEmployee",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Need2ProfessionalDevelopmentEvaluatedEmployee",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "OtherComments",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "PartialEvaluationPeriod",
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
                name: "PeriodRunningActivity",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "ProfessionalTrainingActivities",
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
                name: "SpecialOrMilitaryGrade",
                table: "Evaluations");
        }
    }
}
