using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Renamed_EmployeeFunctions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_GradeEnum_SpecialOrMilitaryGrade",
                table: "Evaluations");

            migrationBuilder.DropTable(
                name: "GradeEnum");

            migrationBuilder.DropTable(
                name: "MaiFunctions");

            migrationBuilder.CreateTable(
                name: "EmployeeFunctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ColaboratorId = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeFunctions_EvaluationTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "EvaluationTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GradesEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradesEnum", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "GradesEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "General_colonel" },
                    { 25, "Comisar" },
                    { 26, "Comisar_principal" },
                    { 27, "Comisar_sef" },
                    { 28, "Chestor" },
                    { 29, "Chestor_sef" },
                    { 30, "Chestor_general" },
                    { 31, "Sergent_inferior_al_serviciului_intern" },
                    { 32, "Sergent_al_serviciului_intern" },
                    { 33, "Sergent_major_al_serviciului_intern" },
                    { 34, "Plutonier_al_serviciului_intern" },
                    { 35, "Plutonier_major_al_serviciului_intern" },
                    { 36, "Plutonier_adjutant_al_serviciului_intern" },
                    { 37, "Locotenent_al_serviciului_intern" },
                    { 38, "Locotenent_major_al_serviciului_intern" },
                    { 39, "Capitan_al_serviciului_intern" },
                    { 40, "Maior_al_serviciului_intern" },
                    { 41, "Locotenent_colonel_al_serviciului_intern" },
                    { 42, "Colonel_al_serviciului_intern" },
                    { 43, "General_maior_al_serviciului_intern" },
                    { 24, "Inspector_principal" },
                    { 44, "General_locotenent_al_serviciului_intern" },
                    { 23, "Inspector_superior" },
                    { 21, "Agent_sef_principal" },
                    { 2, "General_locotenent" },
                    { 3, "General_maior" },
                    { 4, "Colonel" },
                    { 5, "Locotenent_colonel" },
                    { 6, "Maior" },
                    { 7, "Capitan" },
                    { 8, "Locotenent_major" },
                    { 9, "Locotenent_sublocotenent" },
                    { 10, "Plutonier_adjutant" },
                    { 11, "Plutonier_major" },
                    { 12, "Plutonier" },
                    { 13, "Sergent_major" },
                    { 14, "Sergent" },
                    { 15, "Sergent_inferior_caporal_soldat" },
                    { 16, "Agent" },
                    { 17, "Agent_superior" },
                    { 18, "Agent_principal" },
                    { 19, "Agent_sef_adjunct" },
                    { 20, "Agent_sef" },
                    { 22, "Inspector" },
                    { 45, "General_colonel_al_serviciului_intern" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFunctions_Type",
                table: "EmployeeFunctions",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_GradesEnum_Name",
                table: "GradesEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_GradesEnum_SpecialOrMilitaryGrade",
                table: "Evaluations",
                column: "SpecialOrMilitaryGrade",
                principalTable: "GradesEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_GradesEnum_SpecialOrMilitaryGrade",
                table: "Evaluations");

            migrationBuilder.DropTable(
                name: "EmployeeFunctions");

            migrationBuilder.DropTable(
                name: "GradesEnum");

            migrationBuilder.CreateTable(
                name: "GradeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaiFunctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColaboratorId = table.Column<int>(type: "integer", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaiFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaiFunctions_EvaluationTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "EvaluationTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "GradeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Soldat" },
                    { 15, "GeneralDeBrigada" },
                    { 14, "Colonel" },
                    { 13, "LocotenentColonel" },
                    { 12, "Maior" },
                    { 11, "Capitan" },
                    { 10, "LocotenentMajor" },
                    { 16, "GeneralDeDivizie" },
                    { 9, "Locotenent" },
                    { 7, "PlutonierMajor" },
                    { 6, "Plutonier" },
                    { 5, "SergentMajor" },
                    { 4, "Sergent" },
                    { 3, "SergentInferior" },
                    { 2, "Caporal" },
                    { 8, "PlutonierAdjutant" },
                    { 17, "GeneralDeCorpArmata" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeEnum_Name",
                table: "GradeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaiFunctions_Type",
                table: "MaiFunctions",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_GradeEnum_SpecialOrMilitaryGrade",
                table: "Evaluations",
                column: "SpecialOrMilitaryGrade",
                principalTable: "GradeEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
