using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.Evaluation.Data.Persistence.Migrations
{
    public partial class First_PostGreSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TillDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionUnitStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionUnitStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SelectionEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectionEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SequenceEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SequenceEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestingLocationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingLocationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestPassStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestPassStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestResultStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTypeModeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypeModeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTypeSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestTypeId = table.Column<int>(type: "integer", nullable: false),
                    StartWithoutConfirmation = table.Column<bool>(type: "boolean", nullable: false),
                    StartBeforeProgrammation = table.Column<bool>(type: "boolean", nullable: false),
                    StartAfterProgrammation = table.Column<bool>(type: "boolean", nullable: false),
                    PossibleGetToSkipped = table.Column<bool>(type: "boolean", nullable: false),
                    PossibleChangeAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    CanViewResultWithoutVerification = table.Column<bool>(type: "boolean", nullable: false),
                    CanViewPollProgress = table.Column<bool>(type: "boolean", nullable: false),
                    HidePagination = table.Column<bool>(type: "boolean", nullable: false),
                    ShowManyQuestionPerPage = table.Column<bool>(type: "boolean", nullable: false),
                    QuestionsCountPerPage = table.Column<int>(type: "integer", nullable: false),
                    MaxErrors = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypeSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTypeStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypeStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CoreUserId = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    Idnp = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerificationStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlanId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TillDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionCategoryId = table.Column<int>(type: "integer", nullable: false),
                    QuestionType = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: true),
                    QuestionPoints = table.Column<int>(type: "integer", nullable: true),
                    PdfFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaFileId = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionUnits_QuestionCategories_QuestionCategoryId",
                        column: x => x.QuestionCategoryId,
                        principalTable: "QuestionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionUnits_QuestionTypeEnum_QuestionType",
                        column: x => x.QuestionType,
                        principalTable: "QuestionTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionUnits_QuestionUnitStatusEnum_Status",
                        column: x => x.Status,
                        principalTable: "QuestionUnitStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Places = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_TestingLocationType_Type",
                        column: x => x.Type,
                        principalTable: "TestingLocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Rules = table.Column<string>(type: "text", nullable: true),
                    QuestionCount = table.Column<int>(type: "integer", nullable: false),
                    MinPercent = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Mode = table.Column<int>(type: "integer", nullable: false),
                    CategoriesSequence = table.Column<int>(type: "integer", nullable: false),
                    SettingsId = table.Column<int>(type: "integer", nullable: true),
                    PdfFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    Seen = table.Column<bool>(type: "boolean", nullable: false),
                    MessageCode = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanResponsiblePersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlanId = table.Column<int>(type: "integer", nullable: false),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanResponsiblePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanResponsiblePersons_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanResponsiblePersons_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventEvaluators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    EvaluatorId = table.Column<int>(type: "integer", nullable: false),
                    ShowUserName = table.Column<bool>(type: "boolean", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventEvaluators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventEvaluators_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventEvaluators_UserProfiles_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventResponsiblePersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventResponsiblePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventResponsiblePersons_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventResponsiblePersons_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventUsers_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUsers_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionUnitId = table.Column<int>(type: "integer", nullable: false),
                    InternalId = table.Column<int>(type: "integer", nullable: true),
                    Answer = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    MediaFileId = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_QuestionUnits_QuestionUnitId",
                        column: x => x.QuestionUnitId,
                        principalTable: "QuestionUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionUnitTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    QuestionUnitId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionUnitTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionUnitTags_QuestionUnits_QuestionUnitId",
                        column: x => x.QuestionUnitId,
                        principalTable: "QuestionUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionUnitTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventLocations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationClients_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationResponsiblePersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationResponsiblePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationResponsiblePersons_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationResponsiblePersons_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    TestTypeId = table.Column<int>(type: "integer", nullable: false),
                    MaxAttempts = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTestTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTestTypes_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTestTypes_TestTemplates_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    EvaluatorId = table.Column<int>(type: "integer", nullable: true),
                    TestTypeId = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true),
                    TestPassStatus = table.Column<int>(type: "integer", nullable: true),
                    MaxErrors = table.Column<int>(type: "integer", nullable: true),
                    TestStatus = table.Column<int>(type: "integer", nullable: false),
                    ResultStatus = table.Column<int>(type: "integer", nullable: false),
                    AccumulatedPercentage = table.Column<int>(type: "integer", nullable: true),
                    ShowUserName = table.Column<bool>(type: "boolean", nullable: true),
                    ProgrammedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TestTemplatesId = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_TestPassStatusEnum_TestPassStatus",
                        column: x => x.TestPassStatus,
                        principalTable: "TestPassStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_TestResultStatusEnum_ResultStatus",
                        column: x => x.ResultStatus,
                        principalTable: "TestResultStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_TestStatusEnum_TestStatus",
                        column: x => x.TestStatus,
                        principalTable: "TestStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_TestTemplates_TestTemplatesId",
                        column: x => x.TestTemplatesId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_UserProfiles_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestTypeQuestionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestTypeId = table.Column<int>(type: "integer", nullable: false),
                    QuestionCategoryId = table.Column<int>(type: "integer", nullable: false),
                    CategoryIndex = table.Column<int>(type: "integer", nullable: false),
                    QuestionType = table.Column<int>(type: "integer", nullable: true),
                    QuestionCount = table.Column<int>(type: "integer", nullable: true),
                    TimeLimit = table.Column<int>(type: "integer", nullable: true),
                    SelectionType = table.Column<int>(type: "integer", nullable: false),
                    SequenceType = table.Column<int>(type: "integer", nullable: false),
                    TestTemplatesId = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypeQuestionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTypeQuestionCategories_QuestionCategories_QuestionCateg~",
                        column: x => x.QuestionCategoryId,
                        principalTable: "QuestionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestTypeQuestionCategories_QuestionTypeEnum_QuestionType",
                        column: x => x.QuestionType,
                        principalTable: "QuestionTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTypeQuestionCategories_SelectionEnum_SelectionType",
                        column: x => x.SelectionType,
                        principalTable: "SelectionEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTypeQuestionCategories_SequenceEnum_SequenceType",
                        column: x => x.SequenceType,
                        principalTable: "SequenceEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTypeQuestionCategories_TestTemplates_TestTemplatesId",
                        column: x => x.TestTemplatesId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    QuestionUnitId = table.Column<int>(type: "integer", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: false),
                    AnswerStatus = table.Column<int>(type: "integer", nullable: false),
                    Verified = table.Column<int>(type: "integer", nullable: true),
                    TimeLimit = table.Column<int>(type: "integer", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: true),
                    Points = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestions_AnswerStatusEnum_AnswerStatus",
                        column: x => x.AnswerStatus,
                        principalTable: "AnswerStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestQuestions_QuestionUnits_QuestionUnitId",
                        column: x => x.QuestionUnitId,
                        principalTable: "QuestionUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestQuestions_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestQuestions_VerificationStatusEnum_Verified",
                        column: x => x.Verified,
                        principalTable: "VerificationStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestCategoryQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    TestTypeQuestionCategoryId = table.Column<int>(type: "integer", nullable: false),
                    QuestionUnitId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCategoryQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCategoryQuestions_QuestionUnits_QuestionUnitId",
                        column: x => x.QuestionUnitId,
                        principalTable: "QuestionUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestCategoryQuestions_TestTypeQuestionCategories_TestTypeQu~",
                        column: x => x.TestTypeQuestionCategoryId,
                        principalTable: "TestTypeQuestionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestQuestionId = table.Column<int>(type: "integer", nullable: false),
                    OptionId = table.Column<int>(type: "integer", nullable: true),
                    AnswerValue = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAnswers_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestAnswers_TestQuestions_TestQuestionId",
                        column: x => x.TestQuestionId,
                        principalTable: "TestQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AnswerStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "None" },
                    { 1, "Viewed" },
                    { 2, "Skipped" },
                    { 3, "Answered" }
                });

            migrationBuilder.InsertData(
                table: "QuestionTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "FreeText" },
                    { 2, "MultipleAnswers" },
                    { 3, "OneAnswer" },
                    { 4, "HashedAnswer" }
                });

            migrationBuilder.InsertData(
                table: "QuestionUnitStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Inactive" },
                    { 0, "Draft" },
                    { 1, "Active" }
                });

            migrationBuilder.InsertData(
                table: "SelectionEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "All" },
                    { 1, "Select" }
                });

            migrationBuilder.InsertData(
                table: "SequenceEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Random" },
                    { 1, "Strict" }
                });

            migrationBuilder.InsertData(
                table: "TestPassStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Allowed" },
                    { 1, "Forbidden" }
                });

            migrationBuilder.InsertData(
                table: "TestResultStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "NotPassed" },
                    { 0, "NoResult" },
                    { 1, "Passed" }
                });

            migrationBuilder.InsertData(
                table: "TestStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Programmed" },
                    { 1, "AlowedToStart" },
                    { 2, "InProgress" },
                    { 3, "Terminated" },
                    { 4, "Verified" },
                    { 5, "Closed" }
                });

            migrationBuilder.InsertData(
                table: "TestTypeModeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Poll" },
                    { 0, "Test" }
                });

            migrationBuilder.InsertData(
                table: "TestTypeStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Active" },
                    { 1, "Draft" },
                    { 2, "Canceled" }
                });

            migrationBuilder.InsertData(
                table: "TestingLocationType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Class" },
                    { 1, "ComputerClass" }
                });

            migrationBuilder.InsertData(
                table: "VerificationStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Verified" },
                    { 0, "VerifiedBySystem" },
                    { 2, "NotVerified" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerStatusEnum_Name",
                table: "AnswerStatusEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventEvaluators_EvaluatorId",
                table: "EventEvaluators",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EventEvaluators_EventId",
                table: "EventEvaluators",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocations_EventId",
                table: "EventLocations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocations_LocationId",
                table: "EventLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventResponsiblePersons_EventId",
                table: "EventResponsiblePersons",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventResponsiblePersons_UserProfileId",
                table: "EventResponsiblePersons",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PlanId",
                table: "Events",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTestTypes_EventId",
                table: "EventTestTypes",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTestTypes_TestTypeId",
                table: "EventTestTypes",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUsers_EventId",
                table: "EventUsers",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUsers_UserProfileId",
                table: "EventUsers",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationClients_LocationId",
                table: "LocationClients",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationResponsiblePersons_LocationId",
                table: "LocationResponsiblePersons",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationResponsiblePersons_UserProfileId",
                table: "LocationResponsiblePersons",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Type",
                table: "Locations",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserProfileId",
                table: "Notifications",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionUnitId",
                table: "Options",
                column: "QuestionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanResponsiblePersons_PlanId",
                table: "PlanResponsiblePersons",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanResponsiblePersons_UserProfileId",
                table: "PlanResponsiblePersons",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTypeEnum_Name",
                table: "QuestionTypeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionUnits_QuestionCategoryId",
                table: "QuestionUnits",
                column: "QuestionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionUnits_QuestionType",
                table: "QuestionUnits",
                column: "QuestionType");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionUnits_Status",
                table: "QuestionUnits",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionUnitStatusEnum_Name",
                table: "QuestionUnitStatusEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionUnitTags_QuestionUnitId",
                table: "QuestionUnitTags",
                column: "QuestionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionUnitTags_TagId",
                table: "QuestionUnitTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectionEnum_Name",
                table: "SelectionEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SequenceEnum_Name",
                table: "SequenceEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_OptionId",
                table: "TestAnswers",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_TestQuestionId",
                table: "TestAnswers",
                column: "TestQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCategoryQuestions_QuestionUnitId",
                table: "TestCategoryQuestions",
                column: "QuestionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCategoryQuestions_TestTypeQuestionCategoryId",
                table: "TestCategoryQuestions",
                column: "TestTypeQuestionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingLocationType_Name",
                table: "TestingLocationType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestPassStatusEnum_Name",
                table: "TestPassStatusEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_AnswerStatus",
                table: "TestQuestions",
                column: "AnswerStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_QuestionUnitId",
                table: "TestQuestions",
                column: "QuestionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_TestId",
                table: "TestQuestions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_Verified",
                table: "TestQuestions",
                column: "Verified");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultStatusEnum_Name",
                table: "TestResultStatusEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_EvaluatorId",
                table: "Tests",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_EventId",
                table: "Tests",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_LocationId",
                table: "Tests",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_ResultStatus",
                table: "Tests",
                column: "ResultStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestPassStatus",
                table: "Tests",
                column: "TestPassStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestStatus",
                table: "Tests",
                column: "TestStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestTemplatesId",
                table: "Tests",
                column: "TestTemplatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_UserProfileId",
                table: "Tests",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TestStatusEnum_Name",
                table: "TestStatusEnum",
                column: "Name",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeModeEnum_Name",
                table: "TestTypeModeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeQuestionCategories_QuestionCategoryId",
                table: "TestTypeQuestionCategories",
                column: "QuestionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeQuestionCategories_QuestionType",
                table: "TestTypeQuestionCategories",
                column: "QuestionType");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeQuestionCategories_SelectionType",
                table: "TestTypeQuestionCategories",
                column: "SelectionType");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeQuestionCategories_SequenceType",
                table: "TestTypeQuestionCategories",
                column: "SequenceType");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeQuestionCategories_TestTemplatesId",
                table: "TestTypeQuestionCategories",
                column: "TestTemplatesId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeStatusEnum_Name",
                table: "TestTypeStatusEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationStatusEnum_Name",
                table: "VerificationStatusEnum",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "EventEvaluators");

            migrationBuilder.DropTable(
                name: "EventLocations");

            migrationBuilder.DropTable(
                name: "EventResponsiblePersons");

            migrationBuilder.DropTable(
                name: "EventTestTypes");

            migrationBuilder.DropTable(
                name: "EventUsers");

            migrationBuilder.DropTable(
                name: "LocationClients");

            migrationBuilder.DropTable(
                name: "LocationResponsiblePersons");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PlanResponsiblePersons");

            migrationBuilder.DropTable(
                name: "QuestionUnitTags");

            migrationBuilder.DropTable(
                name: "TestAnswers");

            migrationBuilder.DropTable(
                name: "TestCategoryQuestions");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "TestQuestions");

            migrationBuilder.DropTable(
                name: "TestTypeQuestionCategories");

            migrationBuilder.DropTable(
                name: "AnswerStatusEnum");

            migrationBuilder.DropTable(
                name: "QuestionUnits");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "VerificationStatusEnum");

            migrationBuilder.DropTable(
                name: "SelectionEnum");

            migrationBuilder.DropTable(
                name: "QuestionCategories");

            migrationBuilder.DropTable(
                name: "QuestionTypeEnum");

            migrationBuilder.DropTable(
                name: "QuestionUnitStatusEnum");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "TestPassStatusEnum");

            migrationBuilder.DropTable(
                name: "TestResultStatusEnum");

            migrationBuilder.DropTable(
                name: "TestStatusEnum");

            migrationBuilder.DropTable(
                name: "TestTemplates");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "TestingLocationType");

            migrationBuilder.DropTable(
                name: "SequenceEnum");

            migrationBuilder.DropTable(
                name: "TestTypeModeEnum");

            migrationBuilder.DropTable(
                name: "TestTypeSettings");

            migrationBuilder.DropTable(
                name: "TestTypeStatusEnum");
        }
    }
}
