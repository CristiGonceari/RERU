using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.Evaluation.Data.Persistence.Migrations
{
    public partial class Add_Solicited_Tests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "SolicitedTestStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "New" },
                    { 1, "Refused" },
                    { 2, "Approved" }
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitedTests");

            migrationBuilder.DropTable(
                name: "SolicitedTestStatusEnum");
        }
    }
}
