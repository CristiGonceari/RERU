using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Added_Evaluations360_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "EvaluationStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationTypeEnum", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EvaluatedUserProfileId = table.Column<int>(type: "integer", nullable: false),
                    EvaluatorUserProfileId = table.Column<int>(type: "integer", nullable: false),
                    CounterSignerUserProfileId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    Points = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    SubdivisionName = table.Column<string>(type: "text", nullable: true),
                    DateCompletionGeneralData = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NameSurnameEvaluated = table.Column<string>(type: "text", nullable: true),
                    FunctionSubdivision = table.Column<string>(type: "text", nullable: true),
                    SubdivisionEvaluated = table.Column<string>(type: "text", nullable: true),
                    SpecialOrMilitaryGrade = table.Column<int>(type: "integer", nullable: true),
                    SpecialOrMilitaryGradeText = table.Column<string>(type: "text", nullable: true),
                    PeriodEvaluatedFromTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PeriodEvaluatedUpTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EducationEnum = table.Column<int>(type: "integer", nullable: true),
                    ProfessionalTrainingActivities = table.Column<int>(type: "integer", nullable: true),
                    ProfessionalTrainingActivitiesType = table.Column<int>(type: "integer", nullable: true),
                    CourseName = table.Column<string>(type: "text", nullable: true),
                    PeriodRunningActivityFromTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PeriodRunningActivityUpTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AdministrativeActOfStudies = table.Column<string>(type: "text", nullable: true),
                    ServiceDuringEvaluationCourse = table.Column<int>(type: "integer", nullable: true),
                    FunctionEvaluated = table.Column<string>(type: "text", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AdministrativeActService = table.Column<string>(type: "text", nullable: true),
                    PartialEvaluationPeriodFromTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PartialEvaluationPeriodUpTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PartialEvaluationScore = table.Column<decimal>(type: "numeric", nullable: true),
                    FinalScorePartialEvaluations = table.Column<string>(type: "text", nullable: true),
                    QualifierPartialEvaluations = table.Column<int>(type: "integer", nullable: true),
                    SanctionAppliedEvaluationCourse = table.Column<string>(type: "text", nullable: true),
                    DateSanctionApplication = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateLiftingSanction = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    QualificationEvaluationObtained2YearsPast = table.Column<int>(type: "integer", nullable: true),
                    QualificationEvaluationObtainedPreviousYear = table.Column<int>(type: "integer", nullable: true),
                    QualificationQuarter1 = table.Column<int>(type: "integer", nullable: true),
                    QualificationQuarter2 = table.Column<int>(type: "integer", nullable: true),
                    QualificationQuarter3 = table.Column<int>(type: "integer", nullable: true),
                    QualificationQuarter4 = table.Column<int>(type: "integer", nullable: true),
                    Question1 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question2 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question3 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question4 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question5 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question6 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question7 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question8 = table.Column<decimal>(type: "numeric", nullable: true),
                    Goal1 = table.Column<string>(type: "text", nullable: true),
                    Goal2 = table.Column<string>(type: "text", nullable: true),
                    Goal3 = table.Column<string>(type: "text", nullable: true),
                    Goal4 = table.Column<string>(type: "text", nullable: true),
                    Goal5 = table.Column<string>(type: "text", nullable: true),
                    KPI1 = table.Column<string>(type: "text", nullable: true),
                    KPI2 = table.Column<string>(type: "text", nullable: true),
                    KPI3 = table.Column<string>(type: "text", nullable: true),
                    KPI4 = table.Column<string>(type: "text", nullable: true),
                    KPI5 = table.Column<string>(type: "text", nullable: true),
                    PerformanceTerm1 = table.Column<string>(type: "text", nullable: true),
                    PerformanceTerm2 = table.Column<string>(type: "text", nullable: true),
                    PerformanceTerm3 = table.Column<string>(type: "text", nullable: true),
                    PerformanceTerm4 = table.Column<string>(type: "text", nullable: true),
                    PerformanceTerm5 = table.Column<string>(type: "text", nullable: true),
                    Score1 = table.Column<decimal>(type: "numeric", nullable: true),
                    Score2 = table.Column<decimal>(type: "numeric", nullable: true),
                    Score3 = table.Column<decimal>(type: "numeric", nullable: true),
                    Score4 = table.Column<decimal>(type: "numeric", nullable: true),
                    Score5 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question9 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question10 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question11 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question12 = table.Column<decimal>(type: "numeric", nullable: true),
                    Question13 = table.Column<decimal>(type: "numeric", nullable: true),
                    FinalEvaluationQualification = table.Column<int>(type: "integer", nullable: true),
                    DateEvaluationInterview = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateSettingIindividualGoals = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Need1ProfessionalDevelopmentEvaluated = table.Column<string>(type: "text", nullable: true),
                    Need2ProfessionalDevelopmentEvaluated = table.Column<string>(type: "text", nullable: true),
                    Need3ProfessionalDevelopmentEvaluated = table.Column<string>(type: "text", nullable: true),
                    Need4ProfessionalDevelopmentEvaluated = table.Column<string>(type: "text", nullable: true),
                    Need5ProfessionalDevelopmentEvaluated = table.Column<string>(type: "text", nullable: true),
                    CommentsEvaluator = table.Column<string>(type: "text", nullable: true),
                    CommentsEvaluated = table.Column<string>(type: "text", nullable: true),
                    DateAcceptOrRejectEvaluated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SignatureEvaluated = table.Column<bool>(type: "boolean", nullable: true),
                    NameSurnameEvaluator = table.Column<string>(type: "text", nullable: true),
                    FunctionEvaluator = table.Column<string>(type: "text", nullable: true),
                    SignatureEvaluator = table.Column<bool>(type: "boolean", nullable: true),
                    CheckComment1 = table.Column<bool>(type: "boolean", nullable: true),
                    CheckComment2 = table.Column<bool>(type: "boolean", nullable: true),
                    CheckComment3 = table.Column<bool>(type: "boolean", nullable: true),
                    CheckComment4 = table.Column<bool>(type: "boolean", nullable: true),
                    OtherComments = table.Column<string>(type: "text", nullable: true),
                    NameSurnameCounterSigner = table.Column<string>(type: "text", nullable: true),
                    FunctionCounterSigner = table.Column<string>(type: "text", nullable: true),
                    DateCompletionCounterSigner = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SignatureCounterSigner = table.Column<bool>(type: "boolean", nullable: true),
                    DateEvaluatedKnow = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SignatureAcknowledgeEvaluated = table.Column<bool>(type: "boolean", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_EducationEnum_EducationEnum",
                        column: x => x.EducationEnum,
                        principalTable: "EducationEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_EvaluationStatusEnum_Status",
                        column: x => x.Status,
                        principalTable: "EvaluationStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_EvaluationTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "EvaluationTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_GradeEnum_SpecialOrMilitaryGrade",
                        column: x => x.SpecialOrMilitaryGrade,
                        principalTable: "GradeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_ProfessionalTrainingActivitiesEnum_Professional~",
                        column: x => x.ProfessionalTrainingActivities,
                        principalTable: "ProfessionalTrainingActivitiesEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_ProfessionalTrainingActivitiesType_Professional~",
                        column: x => x.ProfessionalTrainingActivitiesType,
                        principalTable: "ProfessionalTrainingActivitiesType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_QualifierEnum_FinalEvaluationQualification",
                        column: x => x.FinalEvaluationQualification,
                        principalTable: "QualifierEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_QualifierEnum_QualificationEvaluationObtained2Y~",
                        column: x => x.QualificationEvaluationObtained2YearsPast,
                        principalTable: "QualifierEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_QualifierEnum_QualificationEvaluationObtainedPr~",
                        column: x => x.QualificationEvaluationObtainedPreviousYear,
                        principalTable: "QualifierEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_QualifierEnum_QualificationQuarter1",
                        column: x => x.QualificationQuarter1,
                        principalTable: "QualifierEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_QualifierEnum_QualificationQuarter2",
                        column: x => x.QualificationQuarter2,
                        principalTable: "QualifierEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_QualifierEnum_QualificationQuarter3",
                        column: x => x.QualificationQuarter3,
                        principalTable: "QualifierEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_QualifierEnum_QualificationQuarter4",
                        column: x => x.QualificationQuarter4,
                        principalTable: "QualifierEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_QualifierEnum_QualifierPartialEvaluations",
                        column: x => x.QualifierPartialEvaluations,
                        principalTable: "QualifierEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_ServiceDuringEvaluationCourse_ServiceDuringEval~",
                        column: x => x.ServiceDuringEvaluationCourse,
                        principalTable: "ServiceDuringEvaluationCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_UserProfiles_CounterSignerUserProfileId",
                        column: x => x.CounterSignerUserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_UserProfiles_EvaluatedUserProfileId",
                        column: x => x.EvaluatedUserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_UserProfiles_EvaluatorUserProfileId",
                        column: x => x.EvaluatorUserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EducationEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Higher" },
                    { 4, "Doctorate" },
                    { 3, "Master" },
                    { 1, "Secondary" }
                });

            migrationBuilder.InsertData(
                table: "EvaluationStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 7, "Finished" },
                    { 1, "Draft" },
                    { 5, "CounterSignAccept" },
                    { 4, "Rejected" },
                    { 3, "Accepted" },
                    { 2, "Confirmed" },
                    { 6, "CounterSignReject" }
                });

            migrationBuilder.InsertData(
                table: "EvaluationTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Execution" },
                    { 2, "Management" }
                });

            migrationBuilder.InsertData(
                table: "GradeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 11, "Capitan" },
                    { 17, "GeneralDeCorpArmata" },
                    { 16, "GeneralDeDivizie" },
                    { 15, "GeneralDeBrigada" },
                    { 14, "Colonel" },
                    { 13, "LocotenentColonel" },
                    { 12, "Maior" },
                    { 10, "LocotenentMajor" },
                    { 8, "PlutonierAdjutant" },
                    { 7, "PlutonierMajor" },
                    { 6, "Plutonier" },
                    { 5, "SergentMajor" },
                    { 4, "Sergent" },
                    { 3, "SergentInferior" },
                    { 2, "Caporal" },
                    { 1, "Soldat" },
                    { 9, "Locotenent" }
                });

            migrationBuilder.InsertData(
                table: "ProfessionalTrainingActivitiesEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Specialization" },
                    { 1, "Perfectiosation" }
                });

            migrationBuilder.InsertData(
                table: "ProfessionalTrainingActivitiesType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Intern" },
                    { 2, "Extern" }
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
                    { 2, "Transfer" },
                    { 1, "Employment" },
                    { 3, "Detachment" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationEnum_Name",
                table: "EducationEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_CounterSignerUserProfileId",
                table: "Evaluations",
                column: "CounterSignerUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EducationEnum",
                table: "Evaluations",
                column: "EducationEnum");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EvaluatedUserProfileId",
                table: "Evaluations",
                column: "EvaluatedUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EvaluatorUserProfileId",
                table: "Evaluations",
                column: "EvaluatorUserProfileId");

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
                name: "IX_Evaluations_Status",
                table: "Evaluations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Type",
                table: "Evaluations",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationStatusEnum_Name",
                table: "EvaluationStatusEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTypeEnum_Name",
                table: "EvaluationTypeEnum",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "EducationEnum");

            migrationBuilder.DropTable(
                name: "EvaluationStatusEnum");

            migrationBuilder.DropTable(
                name: "EvaluationTypeEnum");

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
        }
    }
}
