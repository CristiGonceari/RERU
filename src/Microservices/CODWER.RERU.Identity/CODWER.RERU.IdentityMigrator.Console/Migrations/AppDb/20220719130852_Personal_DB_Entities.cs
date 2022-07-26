﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Personal_DB_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "BaseNomenclatureTypesEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseNomenclatureTypesEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentRoleContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    RoleCount = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentRoleContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentRoleContents_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentRoleContents_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTypeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    From = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HrDocumentTemplateCategories",
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
                    table.PrimaryKey("PK_HrDocumentTemplateCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HrDocumentTemplates",
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
                    table.PrimaryKey("PK_HrDocumentTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HrDocumentTemplates_FileTypeEnum_FileType",
                        column: x => x.FileType,
                        principalTable: "FileTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalCharts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    FromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalCharts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetValueEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheetValueEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacationConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaidLeaveDays = table.Column<int>(type: "integer", nullable: false),
                    NonPaidLeaveDays = table.Column<int>(type: "integer", nullable: false),
                    StudyLeaveDays = table.Column<int>(type: "integer", nullable: false),
                    DeathLeaveDays = table.Column<int>(type: "integer", nullable: false),
                    ChildCareLeaveDays = table.Column<int>(type: "integer", nullable: false),
                    ChildBirthLeaveDays = table.Column<int>(type: "integer", nullable: false),
                    MarriageLeaveDays = table.Column<int>(type: "integer", nullable: false),
                    PaternalistLeaveDays = table.Column<int>(type: "integer", nullable: false),
                    IncludeOffDays = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeHolidayDays = table.Column<bool>(type: "boolean", nullable: false),
                    MondayIsWorkDay = table.Column<bool>(type: "boolean", nullable: false),
                    TuesdayIsWorkDay = table.Column<bool>(type: "boolean", nullable: false),
                    WednesdayIsWorkDay = table.Column<bool>(type: "boolean", nullable: false),
                    ThursdayIsWorkDay = table.Column<bool>(type: "boolean", nullable: false),
                    FridayIsWorkDay = table.Column<bool>(type: "boolean", nullable: false),
                    SaturdayIsWorkDay = table.Column<bool>(type: "boolean", nullable: false),
                    SundayIsWorkDay = table.Column<bool>(type: "boolean", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkHoursEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHoursEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NomenclatureTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    BaseNomenclature = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomenclatureTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NomenclatureTypes_BaseNomenclatureTypesEnum_BaseNomenclature",
                        column: x => x.BaseNomenclature,
                        principalTable: "BaseNomenclatureTypesEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HrDocumentTemplateKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KeyName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    HrDocumentCategoriesId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrDocumentTemplateKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HrDocumentTemplateKeys_HrDocumentTemplateCategories_HrDocum~",
                        column: x => x.HrDocumentCategoriesId,
                        principalTable: "HrDocumentTemplateCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentRoleRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationalChartId = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    ParentDepartmentId = table.Column<int>(type: "integer", nullable: true),
                    ChildDepartmentId = table.Column<int>(type: "integer", nullable: true),
                    ChildRoleId = table.Column<int>(type: "integer", nullable: true),
                    ParentRoleId = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentRoleRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentRoleRelations_Departments_ChildDepartmentId",
                        column: x => x.ChildDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentRoleRelations_Departments_ParentDepartmentId",
                        column: x => x.ParentDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentRoleRelations_OrganizationalCharts_Organizational~",
                        column: x => x.OrganizationalChartId,
                        principalTable: "OrganizationalCharts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentRoleRelations_Roles_ChildRoleId",
                        column: x => x.ChildRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentRoleRelations_Roles_ParentRoleId",
                        column: x => x.ParentRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NomenclatureColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsMandatory = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    NomenclatureTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomenclatureColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NomenclatureColumns_FieldTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "FieldTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NomenclatureColumns_NomenclatureTypes_NomenclatureTypeId",
                        column: x => x.NomenclatureTypeId,
                        principalTable: "NomenclatureTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NomenclatureRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    NomenclatureTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomenclatureRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NomenclatureRecords_NomenclatureTypes_NomenclatureTypeId",
                        column: x => x.NomenclatureTypeId,
                        principalTable: "NomenclatureTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    FatherName = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Sex = table.Column<int>(type: "integer", nullable: false),
                    BloodTypeId = table.Column<int>(type: "integer", nullable: false),
                    UserProfileId = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contractors_NomenclatureRecords_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "NomenclatureRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contractors_SexTypeEnum_Sex",
                        column: x => x.Sex,
                        principalTable: "SexTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contractors_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecordValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomenclatureRecordId = table.Column<int>(type: "integer", nullable: false),
                    NomenclatureColumnId = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    ValueAsBoolean = table.Column<bool>(type: "boolean", nullable: true),
                    ValueAsChar = table.Column<char>(type: "character(1)", nullable: true),
                    ValueAsDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ValueAsDouble = table.Column<double>(type: "double precision", nullable: true),
                    ValueAsText = table.Column<string>(type: "text", nullable: true),
                    ValueAsInteger = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordValues_NomenclatureColumns_NomenclatureColumnId",
                        column: x => x.NomenclatureColumnId,
                        principalTable: "NomenclatureColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordValues_NomenclatureRecords_NomenclatureRecordId",
                        column: x => x.NomenclatureRecordId,
                        principalTable: "NomenclatureRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attestations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attestations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attestations_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MediaFileId = table.Column<string>(type: "text", nullable: true),
                    ContractorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avatars_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Badges_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bonuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bonuses_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "ContactTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contacts_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorDepartments_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_ContractorFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFiles_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    GetGeneralData = table.Column<bool>(type: "boolean", nullable: false),
                    GetBulletinData = table.Column<bool>(type: "boolean", nullable: false),
                    GetStudiesData = table.Column<bool>(type: "boolean", nullable: false),
                    GetCimData = table.Column<bool>(type: "boolean", nullable: false),
                    GetPositionsData = table.Column<bool>(type: "boolean", nullable: false),
                    GetRanksData = table.Column<bool>(type: "boolean", nullable: false),
                    GetFamilyComponentData = table.Column<bool>(type: "boolean", nullable: false),
                    GetTimeSheetTableData = table.Column<bool>(type: "boolean", nullable: false),
                    GetDocumentsDataIdentity = table.Column<bool>(type: "boolean", nullable: false),
                    GetDocumentsDataOrders = table.Column<bool>(type: "boolean", nullable: false),
                    GetDocumentsDataCim = table.Column<bool>(type: "boolean", nullable: false),
                    GetDocumentsDataRequest = table.Column<bool>(type: "boolean", nullable: false),
                    GetDocumentsDataVacation = table.Column<bool>(type: "boolean", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorPermissions_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    NetSalary = table.Column<int>(type: "integer", nullable: false),
                    BrutSalary = table.Column<int>(type: "integer", nullable: false),
                    VacationDays = table.Column<int>(type: "integer", nullable: false),
                    CurrencyTypeId = table.Column<int>(type: "integer", nullable: true),
                    SuperiorId = table.Column<int>(type: "integer", nullable: true),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Contractors_SuperiorId",
                        column: x => x.SuperiorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RelationId = table.Column<int>(type: "integer", nullable: false),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_NomenclatureRecords_RelationId",
                        column: x => x.RelationId,
                        principalTable: "NomenclatureRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Thematic = table.Column<string>(type: "text", nullable: true),
                    InstructorName = table.Column<string>(type: "text", nullable: true),
                    InstructorLastName = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructions_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Penalizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalizations_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    GeneratedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ToDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WorkPlace = table.Column<string>(type: "text", nullable: true),
                    ProbationDayPeriod = table.Column<int>(type: "integer", nullable: false),
                    WorkHours = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Positions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Positions_WorkHoursEnum_WorkHours",
                        column: x => x.WorkHours,
                        principalTable: "WorkHoursEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mentions = table.Column<string>(type: "text", nullable: true),
                    From = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RankRecordId = table.Column<int>(type: "integer", nullable: false),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ranks_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ranks_NomenclatureRecords_RankRecordId",
                        column: x => x.RankRecordId,
                        principalTable: "NomenclatureRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheetTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSheetTables_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSheetTables_TimeSheetValueEnum_Value",
                        column: x => x.Value,
                        principalTable: "TimeSheetValueEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mentions = table.Column<string>(type: "text", nullable: true),
                    FromDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CountDays = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    VacationType = table.Column<int>(type: "integer", nullable: false),
                    VacationRequestId = table.Column<string>(type: "text", nullable: true),
                    VacationOrderId = table.Column<string>(type: "text", nullable: true),
                    Institution = table.Column<string>(type: "text", nullable: true),
                    ChildAge = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacations_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacations_StageStatusEnum_Status",
                        column: x => x.Status,
                        principalTable: "StageStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacations_VacationType_VacationType",
                        column: x => x.VacationType,
                        principalTable: "VacationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DismissalRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    From = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    RequestId = table.Column<string>(type: "text", nullable: true),
                    OrderId = table.Column<string>(type: "text", nullable: true),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DismissalRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DismissalRequests_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DismissalRequests_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DismissalRequests_StageStatusEnum_Status",
                        column: x => x.Status,
                        principalTable: "StageStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BaseNomenclatureTypesEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "StudyType" },
                    { 4, "FamilyComponent" },
                    { 3, "Rank" },
                    { 2, "Currency" },
                    { 1, "BloodTypes" }
                });

            migrationBuilder.InsertData(
                table: "ContactTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "PhoneNumber" },
                    { 2, "Email" }
                });

            migrationBuilder.InsertData(
                table: "FieldTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Character" },
                    { 1, "Text" },
                    { 4, "Double" },
                    { 5, "Boolean" },
                    { 6, "Date" },
                    { 7, "DateTime" },
                    { 8, "Email" },
                    { 3, "Integer" }
                });

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "question");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "testtemplate");

            migrationBuilder.InsertData(
                table: "FileTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "test" },
                    { 10, "cim" },
                    { 9, "order" },
                    { 8, "request" },
                    { 7, "photos" },
                    { 6, "identityfiles" },
                    { 5, "documents" },
                    { 4, "media" },
                    { 11, "procesfile" }
                });

            migrationBuilder.InsertData(
                table: "StageStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "New" },
                    { 2, "Approved" },
                    { 3, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "TimeSheetValueEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 104, "Cn" },
                    { 105, "Cm" },
                    { 106, "Cc" },
                    { 107, "D" },
                    { 113, "Cs" },
                    { 110, "Șt" },
                    { 111, "Dt" },
                    { 112, "A" },
                    { 103, "C" },
                    { 114, "Dh" },
                    { 115, "M" },
                    { 109, "St" },
                    { 102, "Ls" },
                    { 108, "Sp" },
                    { 100, "R" },
                    { 101, "Sn" },
                    { 1, "One" },
                    { 2, "Two" },
                    { 3, "Three" },
                    { 4, "Four" },
                    { 5, "Five" },
                    { 24, "Twenty_Four" },
                    { 7, "Seven" },
                    { 8, "Eight" },
                    { 9, "Nine" },
                    { 10, "Ten" },
                    { 11, "Eleven" },
                    { 6, "Six" },
                    { 12, "Twelve" }
                });

            migrationBuilder.InsertData(
                table: "VacationType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 8, "OwnVacation" },
                    { 7, "Paternal" },
                    { 6, "Marriage" },
                    { 5, "BirthOfTheChild" },
                    { 3, "Death" },
                    { 2, "Studies" },
                    { 1, "PaidAnnual" },
                    { 4, "ChildCare" }
                });

            migrationBuilder.InsertData(
                table: "WorkHoursEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 100, "ByCumulation" },
                    { 12, "Twelve" },
                    { 11, "Eleven" },
                    { 10, "Ten" },
                    { 9, "Nine" },
                    { 7, "Seven" },
                    { 6, "Six" },
                    { 5, "Five" },
                    { 4, "Four" },
                    { 3, "Three" },
                    { 2, "Two" },
                    { 8, "Eight" },
                    { 1, "One" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attestations_ContractorId",
                table: "Attestations",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_ContractorId",
                table: "Avatars",
                column: "ContractorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Badges_ContractorId",
                table: "Badges",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseNomenclatureTypesEnum_Name",
                table: "BaseNomenclatureTypesEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_ContractorId",
                table: "Bonuses",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContractorId",
                table: "Contacts",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Type",
                table: "Contacts",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_ContactTypeEnum_Name",
                table: "ContactTypeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractorDepartments_ContractorId",
                table: "ContractorDepartments",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorDepartments_DepartmentId",
                table: "ContractorDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFiles_ContractorId",
                table: "ContractorFiles",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorPermissions_ContractorId",
                table: "ContractorPermissions",
                column: "ContractorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_BloodTypeId",
                table: "Contractors",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_Sex",
                table: "Contractors",
                column: "Sex");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_UserProfileId",
                table: "Contractors",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractorId",
                table: "Contracts",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_SuperiorId",
                table: "Contracts",
                column: "SuperiorId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRoleContents_DepartmentId",
                table: "DepartmentRoleContents",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRoleContents_RoleId",
                table: "DepartmentRoleContents",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRoleRelations_ChildDepartmentId",
                table: "DepartmentRoleRelations",
                column: "ChildDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRoleRelations_ChildRoleId",
                table: "DepartmentRoleRelations",
                column: "ChildRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRoleRelations_OrganizationalChartId",
                table: "DepartmentRoleRelations",
                column: "OrganizationalChartId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRoleRelations_ParentDepartmentId",
                table: "DepartmentRoleRelations",
                column: "ParentDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRoleRelations_ParentRoleId",
                table: "DepartmentRoleRelations",
                column: "ParentRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DismissalRequests_ContractorId",
                table: "DismissalRequests",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_DismissalRequests_PositionId",
                table: "DismissalRequests",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_DismissalRequests_Status",
                table: "DismissalRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_ContractorId",
                table: "FamilyMembers",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_RelationId",
                table: "FamilyMembers",
                column: "RelationId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldTypeEnum_Name",
                table: "FieldTypeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HrDocumentTemplateKeys_HrDocumentCategoriesId",
                table: "HrDocumentTemplateKeys",
                column: "HrDocumentCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_HrDocumentTemplates_FileType",
                table: "HrDocumentTemplates",
                column: "FileType");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_ContractorId",
                table: "Instructions",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_NomenclatureColumns_NomenclatureTypeId",
                table: "NomenclatureColumns",
                column: "NomenclatureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NomenclatureColumns_Type",
                table: "NomenclatureColumns",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_NomenclatureRecords_NomenclatureTypeId",
                table: "NomenclatureRecords",
                column: "NomenclatureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NomenclatureTypes_BaseNomenclature",
                table: "NomenclatureTypes",
                column: "BaseNomenclature");

            migrationBuilder.CreateIndex(
                name: "IX_Penalizations_ContractorId",
                table: "Penalizations",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_ContractorId",
                table: "Positions",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmentId",
                table: "Positions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_RoleId",
                table: "Positions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_WorkHours",
                table: "Positions",
                column: "WorkHours");

            migrationBuilder.CreateIndex(
                name: "IX_Ranks_ContractorId",
                table: "Ranks",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ranks_RankRecordId",
                table: "Ranks",
                column: "RankRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordValues_NomenclatureColumnId",
                table: "RecordValues",
                column: "NomenclatureColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordValues_NomenclatureRecordId",
                table: "RecordValues",
                column: "NomenclatureRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_StageStatusEnum_Name",
                table: "StageStatusEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetTables_ContractorId",
                table: "TimeSheetTables",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetTables_Value",
                table: "TimeSheetTables",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetValueEnum_Name",
                table: "TimeSheetValueEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_ContractorId",
                table: "Vacations",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_Status",
                table: "Vacations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_VacationType",
                table: "Vacations",
                column: "VacationType");

            migrationBuilder.CreateIndex(
                name: "IX_VacationType_Name",
                table: "VacationType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkHoursEnum_Name",
                table: "WorkHoursEnum",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Attestations");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "Bonuses");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "ContractorDepartments");

            migrationBuilder.DropTable(
                name: "ContractorFiles");

            migrationBuilder.DropTable(
                name: "ContractorPermissions");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "DepartmentRoleContents");

            migrationBuilder.DropTable(
                name: "DepartmentRoleRelations");

            migrationBuilder.DropTable(
                name: "DismissalRequests");

            migrationBuilder.DropTable(
                name: "FamilyMembers");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "HrDocumentTemplateKeys");

            migrationBuilder.DropTable(
                name: "HrDocumentTemplates");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "Penalizations");

            migrationBuilder.DropTable(
                name: "Ranks");

            migrationBuilder.DropTable(
                name: "RecordValues");

            migrationBuilder.DropTable(
                name: "TimeSheetTables");

            migrationBuilder.DropTable(
                name: "VacationConfigurations");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "ContactTypeEnum");

            migrationBuilder.DropTable(
                name: "OrganizationalCharts");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "HrDocumentTemplateCategories");

            migrationBuilder.DropTable(
                name: "NomenclatureColumns");

            migrationBuilder.DropTable(
                name: "TimeSheetValueEnum");

            migrationBuilder.DropTable(
                name: "StageStatusEnum");

            migrationBuilder.DropTable(
                name: "VacationType");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "WorkHoursEnum");

            migrationBuilder.DropTable(
                name: "FieldTypeEnum");

            migrationBuilder.DropTable(
                name: "NomenclatureRecords");

            migrationBuilder.DropTable(
                name: "NomenclatureTypes");

            migrationBuilder.DropTable(
                name: "BaseNomenclatureTypesEnum");

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TestTemplate");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Test");
        }
    }
}