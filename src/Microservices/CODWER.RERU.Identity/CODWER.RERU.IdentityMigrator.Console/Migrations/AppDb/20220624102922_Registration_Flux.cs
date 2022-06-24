using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Registration_Flux : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "UserProfiles",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CandidateCitizenshipId",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CandidateNationalityId",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomePhone",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MobilePhone",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateLanguageLevel",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkPhone",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Region = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    Building = table.Column<string>(type: "text", nullable: true),
                    Apartment = table.Column<string>(type: "text", nullable: true),
                    PostCode = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Autobiographies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_Autobiographies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Autobiographies_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateCitizens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CitizenshipName = table.Column<string>(type: "text", nullable: true),
                    TranslateId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCitizens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateNationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NationalityName = table.Column<string>(type: "text", nullable: true),
                    TranslateId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateNationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KinshipDegreeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KinshipDegreeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KinshipRelationCriminalDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_KinshipRelationCriminalDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KinshipRelationCriminalDatas_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeQuelifiersEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeQuelifiersEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialStatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TranslateId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialStatusTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryObligationTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryObligationTypeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModernLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TranslateId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModernLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecommendationForStudies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Function = table.Column<string>(type: "text", nullable: true),
                    Subdivision = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_RecommendationForStudies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecommendationForStudies_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SexTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SexTypeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateLanguageLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateLanguageLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyFrequencyEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyFrequencyEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TrasnlateId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bulletins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Series = table.Column<string>(type: "text", nullable: true),
                    ReleaseDay = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EmittedBy = table.Column<string>(type: "text", nullable: true),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    BirthPlaceId = table.Column<int>(type: "integer", nullable: true),
                    ParentsResidenceAddressId = table.Column<int>(type: "integer", nullable: true),
                    ResidenceAddressId = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bulletins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bulletins_Addresses_BirthPlaceId",
                        column: x => x.BirthPlaceId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bulletins_Addresses_ParentsResidenceAddressId",
                        column: x => x.ParentsResidenceAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bulletins_Addresses_ResidenceAddressId",
                        column: x => x.ResidenceAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bulletins_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KinshipRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KinshipDegree = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BirthLocation = table.Column<string>(type: "text", nullable: true),
                    Function = table.Column<string>(type: "text", nullable: true),
                    WorkLocation = table.Column<string>(type: "text", nullable: true),
                    ResidenceAddress = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_KinshipRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KinshipRelations_KinshipDegreeEnum_KinshipDegree",
                        column: x => x.KinshipDegree,
                        principalTable: "KinshipDegreeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KinshipRelations_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KinshipRelationWithUserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Function = table.Column<string>(type: "text", nullable: true),
                    KinshipDegree = table.Column<int>(type: "integer", nullable: false),
                    Subdivision = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_KinshipRelationWithUserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KinshipRelationWithUserProfiles_KinshipDegreeEnum_KinshipDe~",
                        column: x => x.KinshipDegree,
                        principalTable: "KinshipDegreeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KinshipRelationWithUserProfiles_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaterialStatusTypeId = table.Column<int>(type: "integer", nullable: false),
                    UserProfileid = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialStatuses_MaterialStatusTypes_MaterialStatusTypeId",
                        column: x => x.MaterialStatusTypeId,
                        principalTable: "MaterialStatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialStatuses_UserProfiles_UserProfileid",
                        column: x => x.UserProfileid,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryObligations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MilitaryObligationType = table.Column<int>(type: "integer", nullable: false),
                    MobilizationYear = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WithdrawalYear = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Efectiv = table.Column<string>(type: "text", nullable: true),
                    MilitarySpecialty = table.Column<string>(type: "text", nullable: true),
                    Degree = table.Column<string>(type: "text", nullable: true),
                    MilitaryBookletSeries = table.Column<string>(type: "text", nullable: true),
                    MilitaryBookletNumber = table.Column<int>(type: "integer", nullable: false),
                    MilitaryBookletReleaseDay = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MilitaryBookletEminentAuthority = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_MilitaryObligations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MilitaryObligations_MilitaryObligationTypeEnum_MilitaryObli~",
                        column: x => x.MilitaryObligationType,
                        principalTable: "MilitaryObligationTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MilitaryObligations_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModernLanguageLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModernLanguageId = table.Column<int>(type: "integer", nullable: false),
                    KnowledgeQuelifiers = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_ModernLanguageLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModernLanguageLevels_KnowledgeQuelifiersEnum_KnowledgeQueli~",
                        column: x => x.KnowledgeQuelifiers,
                        principalTable: "KnowledgeQuelifiersEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModernLanguageLevels_ModernLanguages_ModernLanguageId",
                        column: x => x.ModernLanguageId,
                        principalTable: "ModernLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModernLanguageLevels_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Studies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Institution = table.Column<string>(type: "text", nullable: true),
                    StudyFrequency = table.Column<int>(type: "integer", nullable: true),
                    Faculty = table.Column<string>(type: "text", nullable: true),
                    InstitutionAddress = table.Column<string>(type: "text", nullable: true),
                    Specialty = table.Column<string>(type: "text", nullable: true),
                    YearOfAdmission = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    GraduationYear = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StudyTypeId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_Studies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Studies_StudyFrequencyEnum_StudyFrequency",
                        column: x => x.StudyFrequency,
                        principalTable: "StudyFrequencyEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Studies_StudyTypes_StudyTypeId",
                        column: x => x.StudyTypeId,
                        principalTable: "StudyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Studies_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KinshipDegreeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Father" },
                    { 2, "Mother" },
                    { 3, "Brother" },
                    { 4, "Sister" },
                    { 5, "Wife" },
                    { 6, "Husband" },
                    { 7, "Children" },
                    { 8, "Parent" },
                    { 9, "HusbandsBrothers" },
                    { 10, "HusbandsSisters" },
                    { 11, "WifesSisters" },
                    { 12, "WifesBrothers" }
                });

            migrationBuilder.InsertData(
                table: "KnowledgeQuelifiersEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "VeryGood" },
                    { 2, "Good" },
                    { 1, "BasicKnowledge" }
                });

            migrationBuilder.InsertData(
                table: "MilitaryObligationTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Recruit" },
                    { 2, "Reservist" },
                    { 3, "PerformedMilitaryService" },
                    { 4, "AlternativeService" }
                });

            migrationBuilder.InsertData(
                table: "SexTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "StateLanguageLevel",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Basic" },
                    { 2, "Medium" },
                    { 3, "Good" },
                    { 4, "VeryGood" }
                });

            migrationBuilder.InsertData(
                table: "StudyFrequencyEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Daily" },
                    { 2, "LowFrequency" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CandidateCitizenshipId",
                table: "UserProfiles",
                column: "CandidateCitizenshipId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CandidateNationalityId",
                table: "UserProfiles",
                column: "CandidateNationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_Sex",
                table: "UserProfiles",
                column: "Sex");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_StateLanguageLevel",
                table: "UserProfiles",
                column: "StateLanguageLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Autobiographies_UserProfileId",
                table: "Autobiographies",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bulletins_BirthPlaceId",
                table: "Bulletins",
                column: "BirthPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bulletins_ParentsResidenceAddressId",
                table: "Bulletins",
                column: "ParentsResidenceAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Bulletins_ResidenceAddressId",
                table: "Bulletins",
                column: "ResidenceAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Bulletins_UserProfileId",
                table: "Bulletins",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KinshipDegreeEnum_Name",
                table: "KinshipDegreeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KinshipRelationCriminalDatas_UserProfileId",
                table: "KinshipRelationCriminalDatas",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KinshipRelations_KinshipDegree",
                table: "KinshipRelations",
                column: "KinshipDegree");

            migrationBuilder.CreateIndex(
                name: "IX_KinshipRelations_UserProfileId",
                table: "KinshipRelations",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_KinshipRelationWithUserProfiles_KinshipDegree",
                table: "KinshipRelationWithUserProfiles",
                column: "KinshipDegree");

            migrationBuilder.CreateIndex(
                name: "IX_KinshipRelationWithUserProfiles_UserProfileId",
                table: "KinshipRelationWithUserProfiles",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeQuelifiersEnum_Name",
                table: "KnowledgeQuelifiersEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialStatuses_MaterialStatusTypeId",
                table: "MaterialStatuses",
                column: "MaterialStatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialStatuses_UserProfileid",
                table: "MaterialStatuses",
                column: "UserProfileid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryObligations_MilitaryObligationType",
                table: "MilitaryObligations",
                column: "MilitaryObligationType");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryObligations_UserProfileId",
                table: "MilitaryObligations",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryObligationTypeEnum_Name",
                table: "MilitaryObligationTypeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModernLanguageLevels_KnowledgeQuelifiers",
                table: "ModernLanguageLevels",
                column: "KnowledgeQuelifiers");

            migrationBuilder.CreateIndex(
                name: "IX_ModernLanguageLevels_ModernLanguageId",
                table: "ModernLanguageLevels",
                column: "ModernLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ModernLanguageLevels_UserProfileId",
                table: "ModernLanguageLevels",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationForStudies_UserProfileId",
                table: "RecommendationForStudies",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SexTypeEnum_Name",
                table: "SexTypeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StateLanguageLevel_Name",
                table: "StateLanguageLevel",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Studies_StudyFrequency",
                table: "Studies",
                column: "StudyFrequency");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_StudyTypeId",
                table: "Studies",
                column: "StudyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_UserProfileId",
                table: "Studies",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyFrequencyEnum_Name",
                table: "StudyFrequencyEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_CandidateCitizens_CandidateCitizenshipId",
                table: "UserProfiles",
                column: "CandidateCitizenshipId",
                principalTable: "CandidateCitizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_CandidateNationalities_CandidateNationalityId",
                table: "UserProfiles",
                column: "CandidateNationalityId",
                principalTable: "CandidateNationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_SexTypeEnum_Sex",
                table: "UserProfiles",
                column: "Sex",
                principalTable: "SexTypeEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_StateLanguageLevel_StateLanguageLevel",
                table: "UserProfiles",
                column: "StateLanguageLevel",
                principalTable: "StateLanguageLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_CandidateCitizens_CandidateCitizenshipId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_CandidateNationalities_CandidateNationalityId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_SexTypeEnum_Sex",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_StateLanguageLevel_StateLanguageLevel",
                table: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Autobiographies");

            migrationBuilder.DropTable(
                name: "Bulletins");

            migrationBuilder.DropTable(
                name: "CandidateCitizens");

            migrationBuilder.DropTable(
                name: "CandidateNationalities");

            migrationBuilder.DropTable(
                name: "KinshipRelationCriminalDatas");

            migrationBuilder.DropTable(
                name: "KinshipRelations");

            migrationBuilder.DropTable(
                name: "KinshipRelationWithUserProfiles");

            migrationBuilder.DropTable(
                name: "MaterialStatuses");

            migrationBuilder.DropTable(
                name: "MilitaryObligations");

            migrationBuilder.DropTable(
                name: "ModernLanguageLevels");

            migrationBuilder.DropTable(
                name: "RecommendationForStudies");

            migrationBuilder.DropTable(
                name: "SexTypeEnum");

            migrationBuilder.DropTable(
                name: "StateLanguageLevel");

            migrationBuilder.DropTable(
                name: "Studies");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "KinshipDegreeEnum");

            migrationBuilder.DropTable(
                name: "MaterialStatusTypes");

            migrationBuilder.DropTable(
                name: "MilitaryObligationTypeEnum");

            migrationBuilder.DropTable(
                name: "KnowledgeQuelifiersEnum");

            migrationBuilder.DropTable(
                name: "ModernLanguages");

            migrationBuilder.DropTable(
                name: "StudyFrequencyEnum");

            migrationBuilder.DropTable(
                name: "StudyTypes");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_CandidateCitizenshipId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_CandidateNationalityId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_Sex",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_StateLanguageLevel",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CandidateCitizenshipId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CandidateNationalityId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "HomePhone",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "MobilePhone",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "StateLanguageLevel",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "WorkPhone",
                table: "UserProfiles");
        }
    }
}
