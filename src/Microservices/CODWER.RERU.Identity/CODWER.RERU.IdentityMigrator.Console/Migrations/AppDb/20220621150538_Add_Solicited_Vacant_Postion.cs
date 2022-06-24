using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_Solicited_Vacant_Postion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitedTests");

            migrationBuilder.AddColumn<int>(
                name: "RequiredDocumentId",
                table: "UserFiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventVacantPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    CandidatePositionId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventVacantPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventVacantPositions_CandidatePositions_CandidatePositionId",
                        column: x => x.CandidatePositionId,
                        principalTable: "CandidatePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventVacantPositions_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Mandatory = table.Column<bool>(type: "boolean", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitedVacantPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: true),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    TestTemplateId = table.Column<int>(type: "integer", nullable: false),
                    CandidatePositionId = table.Column<int>(type: "integer", nullable: true),
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
                    table.PrimaryKey("PK_SolicitedVacantPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitedVacantPositions_CandidatePositions_CandidatePositi~",
                        column: x => x.CandidatePositionId,
                        principalTable: "CandidatePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitedVacantPositions_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitedVacantPositions_SolicitedTestStatusEnum_SolicitedT~",
                        column: x => x.SolicitedTestStatus,
                        principalTable: "SolicitedTestStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitedVacantPositions_TestTemplates_TestTemplateId",
                        column: x => x.TestTemplateId,
                        principalTable: "TestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitedVacantPositions_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredDocumentPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CandidatePositionId = table.Column<int>(type: "integer", nullable: false),
                    RequiredDocumentId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredDocumentPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequiredDocumentPositions_CandidatePositions_CandidatePosit~",
                        column: x => x.CandidatePositionId,
                        principalTable: "CandidatePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredDocumentPositions_RequiredDocuments_RequiredDocumen~",
                        column: x => x.RequiredDocumentId,
                        principalTable: "RequiredDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_RequiredDocumentId",
                table: "UserFiles",
                column: "RequiredDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_EventVacantPositions_CandidatePositionId",
                table: "EventVacantPositions",
                column: "CandidatePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_EventVacantPositions_EventId",
                table: "EventVacantPositions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredDocumentPositions_CandidatePositionId",
                table: "RequiredDocumentPositions",
                column: "CandidatePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredDocumentPositions_RequiredDocumentId",
                table: "RequiredDocumentPositions",
                column: "RequiredDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedVacantPositions_CandidatePositionId",
                table: "SolicitedVacantPositions",
                column: "CandidatePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedVacantPositions_EventId",
                table: "SolicitedVacantPositions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedVacantPositions_SolicitedTestStatus",
                table: "SolicitedVacantPositions",
                column: "SolicitedTestStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedVacantPositions_TestTemplateId",
                table: "SolicitedVacantPositions",
                column: "TestTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedVacantPositions_UserProfileId",
                table: "SolicitedVacantPositions",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_RequiredDocuments_RequiredDocumentId",
                table: "UserFiles",
                column: "RequiredDocumentId",
                principalTable: "RequiredDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_RequiredDocuments_RequiredDocumentId",
                table: "UserFiles");

            migrationBuilder.DropTable(
                name: "EventVacantPositions");

            migrationBuilder.DropTable(
                name: "RequiredDocumentPositions");

            migrationBuilder.DropTable(
                name: "SolicitedVacantPositions");

            migrationBuilder.DropTable(
                name: "RequiredDocuments");

            migrationBuilder.DropIndex(
                name: "IX_UserFiles_RequiredDocumentId",
                table: "UserFiles");

            migrationBuilder.DropColumn(
                name: "RequiredDocumentId",
                table: "UserFiles");

            migrationBuilder.CreateTable(
                name: "SolicitedTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CandidatePositionId = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EventId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    SolicitedTestStatus = table.Column<int>(type: "integer", nullable: false),
                    SolicitedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TestTemplateId = table.Column<int>(type: "integer", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitedTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitedTests_CandidatePositions_CandidatePositionId",
                        column: x => x.CandidatePositionId,
                        principalTable: "CandidatePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedTests_CandidatePositionId",
                table: "SolicitedTests",
                column: "CandidatePositionId");

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
        }
    }
}
