using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Registration_Flux_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrationFluxStepEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationFluxStepEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationFluxSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false),
                    Step = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_RegistrationFluxSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationFluxSteps_RegistrationFluxStepEnum_Step",
                        column: x => x.Step,
                        principalTable: "RegistrationFluxStepEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrationFluxSteps_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RegistrationFluxStepEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "GeneralData" },
                    { 2, "Bulletin" },
                    { 3, "Study" },
                    { 4, "MaterialStatus" },
                    { 5, "MilitaryObligation" },
                    { 6, "Autobiography" },
                    { 7, "Declaration" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFluxStepEnum_Name",
                table: "RegistrationFluxStepEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFluxSteps_Step",
                table: "RegistrationFluxSteps",
                column: "Step");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFluxSteps_UserProfileId",
                table: "RegistrationFluxSteps",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationFluxSteps");

            migrationBuilder.DropTable(
                name: "RegistrationFluxStepEnum");
        }
    }
}
