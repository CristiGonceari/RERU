using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Evaluation.Data.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionUnitStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionUnitStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SelectionEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectionEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SequenceEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SequenceEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestingLocationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingLocationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestPassStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestPassStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestResultStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTypeModeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypeModeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTypeStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypeStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoreUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerificationStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionCategoryId = table.Column<int>(type: "int", nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionPoints = table.Column<int>(type: "int", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Places = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "TestTypes",
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
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    Seen = table.Column<bool>(type: "bit", nullable: false),
                    MessageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "UserProfileIdentities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identificator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileIdentities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileIdentities_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventEvaluators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false),
                    ShowUserName = table.Column<bool>(type: "bit", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionUnitId = table.Column<int>(type: "int", nullable: false),
                    InternalId = table.Column<int>(type: "int", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    QuestionUnitId = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    TestTypeId = table.Column<int>(type: "int", nullable: false),
                    MaxAttempts = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                        name: "FK_EventTestTypes_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    EvaluatorId = table.Column<int>(type: "int", nullable: true),
                    TestTypeId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    TestPassStatus = table.Column<int>(type: "int", nullable: true),
                    MaxErrors = table.Column<int>(type: "int", nullable: true),
                    TestStatus = table.Column<int>(type: "int", nullable: false),
                    ResultStatus = table.Column<int>(type: "int", nullable: false),
                    AccumulatedPercentage = table.Column<int>(type: "int", nullable: true),
                    ShowUserName = table.Column<bool>(type: "bit", nullable: true),
                    ProgrammedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                        name: "FK_Tests_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestTypeId = table.Column<int>(type: "int", nullable: false),
                    QuestionCategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryIndex = table.Column<int>(type: "int", nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: true),
                    QuestionCount = table.Column<int>(type: "int", nullable: true),
                    TimeLimit = table.Column<int>(type: "int", nullable: true),
                    SelectionType = table.Column<int>(type: "int", nullable: false),
                    SequenceType = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypeQuestionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTypeQuestionCategories_QuestionCategories_QuestionCategoryId",
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
                        name: "FK_TestTypeQuestionCategories_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestTypeSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestTypeId = table.Column<int>(type: "int", nullable: false),
                    StartWithoutConfirmation = table.Column<bool>(type: "bit", nullable: false),
                    StartBeforeProgrammation = table.Column<bool>(type: "bit", nullable: false),
                    StartAfterProgrammation = table.Column<bool>(type: "bit", nullable: false),
                    PossibleGetToSkipped = table.Column<bool>(type: "bit", nullable: false),
                    PossibleChangeAnswer = table.Column<bool>(type: "bit", nullable: false),
                    CanViewResultWithoutVerification = table.Column<bool>(type: "bit", nullable: false),
                    CanViewPollProgress = table.Column<bool>(type: "bit", nullable: false),
                    HidePagination = table.Column<bool>(type: "bit", nullable: false),
                    ShowManyQuestionPerPage = table.Column<bool>(type: "bit", nullable: false),
                    QuestionsCountPerPage = table.Column<int>(type: "int", nullable: false),
                    MaxErrors = table.Column<int>(type: "int", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypeSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTypeSettings_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    QuestionUnitId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    AnswerStatus = table.Column<int>(type: "int", nullable: false),
                    Verified = table.Column<int>(type: "int", nullable: true),
                    TimeLimit = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    TestTypeQuestionCategoryId = table.Column<int>(type: "int", nullable: false),
                    QuestionUnitId = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                        name: "FK_TestCategoryQuestions_TestTypeQuestionCategories_TestTypeQuestionCategoryId",
                        column: x => x.TestTypeQuestionCategoryId,
                        principalTable: "TestTypeQuestionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TestAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestQuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: true),
                    AnswerValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    { 2, "MultiplyAnswers" },
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
                unique: true,
                filter: "[Name] IS NOT NULL");

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
                unique: true,
                filter: "[Name] IS NOT NULL");

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
                unique: true,
                filter: "[Name] IS NOT NULL");

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
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SequenceEnum_Name",
                table: "SequenceEnum",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

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
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TestPassStatusEnum_Name",
                table: "TestPassStatusEnum",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

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
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_EvaluatorId",
                table: "Tests",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_EventId",
                table: "Tests",
                column: "EventId");

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
                name: "IX_Tests_TestTypeId",
                table: "Tests",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_UserProfileId",
                table: "Tests",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TestStatusEnum_Name",
                table: "TestStatusEnum",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeModeEnum_Name",
                table: "TestTypeModeEnum",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

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
                name: "IX_TestTypeQuestionCategories_TestTypeId",
                table: "TestTypeQuestionCategories",
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

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeSettings_TestTypeId",
                table: "TestTypeSettings",
                column: "TestTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestTypeStatusEnum_Name",
                table: "TestTypeStatusEnum",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileIdentities_UserProfileId",
                table: "UserProfileIdentities",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationStatusEnum_Name",
                table: "VerificationStatusEnum",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
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
                name: "TestTypeSettings");

            migrationBuilder.DropTable(
                name: "UserProfileIdentities");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "TestQuestions");

            migrationBuilder.DropTable(
                name: "TestTypeQuestionCategories");

            migrationBuilder.DropTable(
                name: "TestingLocationType");

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
                name: "TestPassStatusEnum");

            migrationBuilder.DropTable(
                name: "TestResultStatusEnum");

            migrationBuilder.DropTable(
                name: "TestStatusEnum");

            migrationBuilder.DropTable(
                name: "TestTypes");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "SequenceEnum");

            migrationBuilder.DropTable(
                name: "TestTypeModeEnum");

            migrationBuilder.DropTable(
                name: "TestTypeStatusEnum");
        }
    }
}
