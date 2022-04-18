using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Init : Migration
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
                name: "CandidatePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatePositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoreArticles",
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
                    table.PrimaryKey("PK_CoreArticles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationArticles",
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
                    table.PrimaryKey("PK_EvaluationArticles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTypeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleTypeEnum", x => x.Id);
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
                name: "RoleTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypeEnum", x => x.Id);
                });

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
                name: "SolicitedTestStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitedTestStatusEnum", x => x.Id);
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
                name: "TestTemplateModeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTemplateModeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTemplateStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTemplateStatusEnum", x => x.Id);
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
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CoreUserId = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    FatherName = table.Column<string>(type: "text", nullable: true),
                    Idnp = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    MediaFileId = table.Column<string>(type: "text", nullable: true),
                    RequiresDataEntry = table.Column<bool>(type: "boolean", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TokenLifetime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CandidatePositionId = table.Column<int>(type: "integer", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_UserProfiles_CandidatePositions_CandidatePositionId",
                        column: x => x.CandidatePositionId,
                        principalTable: "CandidatePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTemplateKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KeyName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FileType = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTemplateKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTemplateKeys_FileTypeEnum_FileType",
                        column: x => x.FileType,
                        principalTable: "FileTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    FileType = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTemplates_FileTypeEnum_FileType",
                        column: x => x.FileType,
                        principalTable: "FileTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PublicUrl = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    InternalGatewayAPIPath = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_ModuleStatus_Status",
                        column: x => x.Status,
                        principalTable: "ModuleStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Modules_ModuleTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "ModuleTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TillDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PlanId = table.Column<int>(type: "integer", nullable: true),
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
                    Question = table.Column<string>(type: "text", nullable: true),
                    QuestionPoints = table.Column<int>(type: "integer", nullable: true),
                    MediaFileId = table.Column<string>(type: "text", nullable: true),
                    QuestionType = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    QuestionCategoryId = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_TestTemplates_TestTemplateModeEnum_Mode",
                        column: x => x.Mode,
                        principalTable: "TestTemplateModeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTemplates_TestTemplateStatusEnum_Status",
                        column: x => x.Status,
                        principalTable: "TestTemplateStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Seen = table.Column<bool>(type: "boolean", nullable: false),
                    MessageCode = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
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
                name: "UserFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    FileId = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFiles_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileIdentity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Identificator = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileIdentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileIdentity_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModulePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModulePermissions_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    IsAssignByDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleRoles_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleRoles_RoleTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "RoleTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventEvaluators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShowUserName = table.Column<bool>(type: "boolean", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    EvaluatorId = table.Column<int>(type: "integer", nullable: false),
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
                    InternalId = table.Column<int>(type: "integer", nullable: true),
                    Answer = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    MediaFileId = table.Column<string>(type: "text", nullable: true),
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
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
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
                name: "EventTestTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaxAttempts = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    TestTemplateId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTestTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTestTemplates_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTestTemplates_TestTemplates_TestTemplateId",
                        column: x => x.TestTemplateId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitedTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: true),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    TestTemplateId = table.Column<int>(type: "integer", nullable: false),
                    SolicitedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SolicitedTestStatus = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitedTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitedTests_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitedTests_SolicitedTestStatusEnum_SolicitedTestStatus",
                        column: x => x.SolicitedTestStatus,
                        principalTable: "SolicitedTestStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitedTests_TestTemplates_TestTemplateId",
                        column: x => x.TestTemplateId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitedTests_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccumulatedPercentage = table.Column<int>(type: "integer", nullable: true),
                    ShowUserName = table.Column<bool>(type: "boolean", nullable: true),
                    MaxErrors = table.Column<int>(type: "integer", nullable: true),
                    TestPassStatus = table.Column<int>(type: "integer", nullable: true),
                    TestStatus = table.Column<int>(type: "integer", nullable: false),
                    ResultStatus = table.Column<int>(type: "integer", nullable: false),
                    ProgrammedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    EvaluatorId = table.Column<int>(type: "integer", nullable: true),
                    TestTemplateId = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true),
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
                        name: "FK_Tests_TestTemplates_TestTemplateId",
                        column: x => x.TestTemplateId,
                        principalTable: "TestTemplates",
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
                name: "TestTemplateQuestionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryIndex = table.Column<int>(type: "integer", nullable: false),
                    QuestionCount = table.Column<int>(type: "integer", nullable: true),
                    TimeLimit = table.Column<int>(type: "integer", nullable: true),
                    QuestionType = table.Column<int>(type: "integer", nullable: true),
                    SelectionType = table.Column<int>(type: "integer", nullable: false),
                    SequenceType = table.Column<int>(type: "integer", nullable: false),
                    TestTemplateId = table.Column<int>(type: "integer", nullable: false),
                    QuestionCategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTemplateQuestionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTemplateQuestionCategories_QuestionCategories_QuestionC~",
                        column: x => x.QuestionCategoryId,
                        principalTable: "QuestionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestTemplateQuestionCategories_QuestionTypeEnum_QuestionType",
                        column: x => x.QuestionType,
                        principalTable: "QuestionTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTemplateQuestionCategories_SelectionEnum_SelectionType",
                        column: x => x.SelectionType,
                        principalTable: "SelectionEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTemplateQuestionCategories_SequenceEnum_SequenceType",
                        column: x => x.SequenceType,
                        principalTable: "SequenceEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTemplateQuestionCategories_TestTemplates_TestTemplateId",
                        column: x => x.TestTemplateId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestTemplateSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestTemplateId = table.Column<int>(type: "integer", nullable: false),
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
                    FormulaForOneAnswer = table.Column<int>(type: "integer", nullable: false),
                    NegativeScoreForOneAnswer = table.Column<bool>(type: "boolean", nullable: true),
                    FormulaForMultipleAnswers = table.Column<int>(type: "integer", nullable: false),
                    NegativeScoreForMultipleAnswers = table.Column<bool>(type: "boolean", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTemplateSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTemplateSettings_ScoreFormulaEnum_FormulaForMultipleAns~",
                        column: x => x.FormulaForMultipleAnswers,
                        principalTable: "ScoreFormulaEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTemplateSettings_ScoreFormulaEnum_FormulaForOneAnswer",
                        column: x => x.FormulaForOneAnswer,
                        principalTable: "ScoreFormulaEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTemplateSettings_TestTemplates_TestTemplateId",
                        column: x => x.TestTemplateId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleRolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleRoleId = table.Column<int>(type: "integer", nullable: false),
                    ModulePermissionId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleRolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleRolePermissions_ModulePermissions_ModulePermissionId",
                        column: x => x.ModulePermissionId,
                        principalTable: "ModulePermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleRolePermissions_ModuleRoles_ModuleRoleId",
                        column: x => x.ModuleRoleId,
                        principalTable: "ModuleRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileModuleRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    ModuleRoleId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileModuleRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileModuleRoles_ModuleRoles_ModuleRoleId",
                        column: x => x.ModuleRoleId,
                        principalTable: "ModuleRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfileModuleRoles_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailTestNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_EmailTestNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTestNotifications_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailTestNotifications_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    TimeLimit = table.Column<int>(type: "integer", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: true),
                    Points = table.Column<int>(type: "integer", nullable: true),
                    AnswerStatus = table.Column<int>(type: "integer", nullable: false),
                    Verified = table.Column<int>(type: "integer", nullable: true),
                    QuestionUnitId = table.Column<int>(type: "integer", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: false),
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
                    TestTemplateQuestionCategoryId = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_TestCategoryQuestions_TestTemplateQuestionCategories_TestTe~",
                        column: x => x.TestTemplateQuestionCategoryId,
                        principalTable: "TestTemplateQuestionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnswerValue = table.Column<string>(type: "text", nullable: true),
                    TestQuestionId = table.Column<int>(type: "integer", nullable: false),
                    OptionId = table.Column<int>(type: "integer", nullable: true),
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
                table: "FileTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "TestTemplate" },
                    { 2, "Test" }
                });

            migrationBuilder.InsertData(
                table: "ModuleStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Offline" },
                    { 1, "Online" }
                });

            migrationBuilder.InsertData(
                table: "ModuleTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Dynamic" },
                    { 1, "Default" }
                });

            migrationBuilder.InsertData(
                table: "QuestionTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "HashedAnswer" },
                    { 3, "OneAnswer" },
                    { 1, "FreeText" },
                    { 2, "MultipleAnswers" }
                });

            migrationBuilder.InsertData(
                table: "QuestionUnitStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Draft" },
                    { 1, "Active" },
                    { 2, "Inactive" }
                });

            migrationBuilder.InsertData(
                table: "RoleTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Default" },
                    { 2, "Dynamic" }
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

            migrationBuilder.InsertData(
                table: "SelectionEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Select" },
                    { 0, "All" }
                });

            migrationBuilder.InsertData(
                table: "SequenceEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Strict" },
                    { 0, "Random" }
                });

            migrationBuilder.InsertData(
                table: "SolicitedTestStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "New" },
                    { 1, "Refused" },
                    { 2, "Approved" }
                });

            migrationBuilder.InsertData(
                table: "TestPassStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Forbidden" },
                    { 2, "Allowed" }
                });

            migrationBuilder.InsertData(
                table: "TestResultStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "NoResult" },
                    { 1, "Passed" },
                    { 2, "NotPassed" }
                });

            migrationBuilder.InsertData(
                table: "TestStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Closed" },
                    { 4, "Verified" },
                    { 3, "Terminated" },
                    { 1, "AlowedToStart" },
                    { 0, "Programmed" },
                    { 2, "InProgress" }
                });

            migrationBuilder.InsertData(
                table: "TestTemplateModeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Test" },
                    { 1, "Poll" }
                });

            migrationBuilder.InsertData(
                table: "TestTemplateStatusEnum",
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
                name: "IX_DocumentTemplateKeys_FileType",
                table: "DocumentTemplateKeys",
                column: "FileType");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplates_FileType",
                table: "DocumentTemplates",
                column: "FileType");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTestNotifications_TestId",
                table: "EmailTestNotifications",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTestNotifications_UserProfileId",
                table: "EmailTestNotifications",
                column: "UserProfileId");

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
                name: "IX_EventTestTemplates_EventId",
                table: "EventTestTemplates",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTestTemplates_TestTemplateId",
                table: "EventTestTemplates",
                column: "TestTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUsers_EventId",
                table: "EventUsers",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUsers_UserProfileId",
                table: "EventUsers",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileTypeEnum_Name",
                table: "FileTypeEnum",
                column: "Name",
                unique: true);

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
                name: "IX_ModulePermissions_ModuleId",
                table: "ModulePermissions",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleRolePermissions_ModulePermissionId",
                table: "ModuleRolePermissions",
                column: "ModulePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleRolePermissions_ModuleRoleId",
                table: "ModuleRolePermissions",
                column: "ModuleRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleRoles_ModuleId",
                table: "ModuleRoles",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleRoles_Type",
                table: "ModuleRoles",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Status",
                table: "Modules",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Type",
                table: "Modules",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleStatus_Name",
                table: "ModuleStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleTypeEnum_Name",
                table: "ModuleTypeEnum",
                column: "Name",
                unique: true);

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
                name: "IX_RoleTypeEnum_Name",
                table: "RoleTypeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScoreFormulaEnum_Name",
                table: "ScoreFormulaEnum",
                column: "Name",
                unique: true);

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
                name: "IX_SolicitedTests_EventId",
                table: "SolicitedTests",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedTests_SolicitedTestStatus",
                table: "SolicitedTests",
                column: "SolicitedTestStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedTests_TestTemplateId",
                table: "SolicitedTests",
                column: "TestTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedTests_UserProfileId",
                table: "SolicitedTests",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedTestStatusEnum_Name",
                table: "SolicitedTestStatusEnum",
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
                name: "IX_TestCategoryQuestions_TestTemplateQuestionCategoryId",
                table: "TestCategoryQuestions",
                column: "TestTemplateQuestionCategoryId");

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
                name: "IX_Tests_TestTemplateId",
                table: "Tests",
                column: "TestTemplateId");

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
                name: "IX_TestTemplateModeEnum_Name",
                table: "TestTemplateModeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateQuestionCategories_QuestionCategoryId",
                table: "TestTemplateQuestionCategories",
                column: "QuestionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateQuestionCategories_QuestionType",
                table: "TestTemplateQuestionCategories",
                column: "QuestionType");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateQuestionCategories_SelectionType",
                table: "TestTemplateQuestionCategories",
                column: "SelectionType");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateQuestionCategories_SequenceType",
                table: "TestTemplateQuestionCategories",
                column: "SequenceType");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateQuestionCategories_TestTemplateId",
                table: "TestTemplateQuestionCategories",
                column: "TestTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplates_CategoriesSequence",
                table: "TestTemplates",
                column: "CategoriesSequence");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplates_Mode",
                table: "TestTemplates",
                column: "Mode");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplates_Status",
                table: "TestTemplates",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateSettings_FormulaForMultipleAnswers",
                table: "TestTemplateSettings",
                column: "FormulaForMultipleAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateSettings_FormulaForOneAnswer",
                table: "TestTemplateSettings",
                column: "FormulaForOneAnswer");

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateSettings_TestTemplateId",
                table: "TestTemplateSettings",
                column: "TestTemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestTemplateStatusEnum_Name",
                table: "TestTemplateStatusEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_UserProfileId",
                table: "UserFiles",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileIdentity_UserProfileId",
                table: "UserProfileIdentity",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileModuleRoles_ModuleRoleId",
                table: "UserProfileModuleRoles",
                column: "ModuleRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileModuleRoles_UserProfileId",
                table: "UserProfileModuleRoles",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CandidatePositionId",
                table: "UserProfiles",
                column: "CandidatePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationStatusEnum_Name",
                table: "VerificationStatusEnum",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoreArticles");

            migrationBuilder.DropTable(
                name: "DocumentTemplateKeys");

            migrationBuilder.DropTable(
                name: "DocumentTemplates");

            migrationBuilder.DropTable(
                name: "EmailTestNotifications");

            migrationBuilder.DropTable(
                name: "EvaluationArticles");

            migrationBuilder.DropTable(
                name: "EventEvaluators");

            migrationBuilder.DropTable(
                name: "EventLocations");

            migrationBuilder.DropTable(
                name: "EventResponsiblePersons");

            migrationBuilder.DropTable(
                name: "EventTestTemplates");

            migrationBuilder.DropTable(
                name: "EventUsers");

            migrationBuilder.DropTable(
                name: "LocationClients");

            migrationBuilder.DropTable(
                name: "LocationResponsiblePersons");

            migrationBuilder.DropTable(
                name: "ModuleRolePermissions");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PlanResponsiblePersons");

            migrationBuilder.DropTable(
                name: "QuestionUnitTags");

            migrationBuilder.DropTable(
                name: "SolicitedTests");

            migrationBuilder.DropTable(
                name: "TestAnswers");

            migrationBuilder.DropTable(
                name: "TestCategoryQuestions");

            migrationBuilder.DropTable(
                name: "TestTemplateSettings");

            migrationBuilder.DropTable(
                name: "UserFiles");

            migrationBuilder.DropTable(
                name: "UserProfileIdentity");

            migrationBuilder.DropTable(
                name: "UserProfileModuleRoles");

            migrationBuilder.DropTable(
                name: "FileTypeEnum");

            migrationBuilder.DropTable(
                name: "ModulePermissions");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "SolicitedTestStatusEnum");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "TestQuestions");

            migrationBuilder.DropTable(
                name: "TestTemplateQuestionCategories");

            migrationBuilder.DropTable(
                name: "ScoreFormulaEnum");

            migrationBuilder.DropTable(
                name: "ModuleRoles");

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
                name: "Modules");

            migrationBuilder.DropTable(
                name: "RoleTypeEnum");

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
                name: "ModuleStatus");

            migrationBuilder.DropTable(
                name: "ModuleTypeEnum");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "TestingLocationType");

            migrationBuilder.DropTable(
                name: "SequenceEnum");

            migrationBuilder.DropTable(
                name: "TestTemplateModeEnum");

            migrationBuilder.DropTable(
                name: "TestTemplateStatusEnum");

            migrationBuilder.DropTable(
                name: "CandidatePositions");
        }
    }
}
