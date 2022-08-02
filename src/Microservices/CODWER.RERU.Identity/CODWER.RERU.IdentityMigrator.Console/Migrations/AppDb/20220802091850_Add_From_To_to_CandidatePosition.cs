using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_From_To_to_CandidatePosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "RegistrationFluxSteps",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CandidatePositionId",
                table: "EventUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "EventUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                table: "CandidatePositions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "To",
                table: "CandidatePositions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFluxSteps_UserProfileId",
                table: "RegistrationFluxSteps",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUsers_CandidatePositionId",
                table: "EventUsers",
                column: "CandidatePositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventUsers_CandidatePositions_CandidatePositionId",
                table: "EventUsers",
                column: "CandidatePositionId",
                principalTable: "CandidatePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationFluxSteps_UserProfiles_UserProfileId",
                table: "RegistrationFluxSteps",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventUsers_CandidatePositions_CandidatePositionId",
                table: "EventUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationFluxSteps_UserProfiles_UserProfileId",
                table: "RegistrationFluxSteps");

            migrationBuilder.DropIndex(
                name: "IX_RegistrationFluxSteps_UserProfileId",
                table: "RegistrationFluxSteps");

            migrationBuilder.DropIndex(
                name: "IX_EventUsers_CandidatePositionId",
                table: "EventUsers");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "RegistrationFluxSteps");

            migrationBuilder.DropColumn(
                name: "CandidatePositionId",
                table: "EventUsers");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "EventUsers");

            migrationBuilder.DropColumn(
                name: "From",
                table: "CandidatePositions");

            migrationBuilder.DropColumn(
                name: "To",
                table: "CandidatePositions");
        }
    }
}
