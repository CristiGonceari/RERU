using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_Event_User_Candidate_Position_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventUserCandidatePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventUserId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_EventUserCandidatePositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventUserCandidatePositions_CandidatePositions_CandidatePos~",
                        column: x => x.CandidatePositionId,
                        principalTable: "CandidatePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventUserCandidatePositions_EventUsers_EventUserId",
                        column: x => x.EventUserId,
                        principalTable: "EventUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventUserCandidatePositions_CandidatePositionId",
                table: "EventUserCandidatePositions",
                column: "CandidatePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUserCandidatePositions_EventUserId",
                table: "EventUserCandidatePositions",
                column: "EventUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventUserCandidatePositions");
        }
    }
}
