using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.Core.Data.Persistence.Migrations
{
    public partial class Add_Candidate_Position : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidatePositionId",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CandidatePositionId",
                table: "UserProfiles",
                column: "CandidatePositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_CandidatePositions_CandidatePositionId",
                table: "UserProfiles",
                column: "CandidatePositionId",
                principalTable: "CandidatePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_CandidatePositions_CandidatePositionId",
                table: "UserProfiles");

            migrationBuilder.DropTable(
                name: "CandidatePositions");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_CandidatePositionId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CandidatePositionId",
                table: "UserProfiles");
        }
    }
}
