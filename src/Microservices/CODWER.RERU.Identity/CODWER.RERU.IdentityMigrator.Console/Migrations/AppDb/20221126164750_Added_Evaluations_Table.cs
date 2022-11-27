using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Added_Evaluations_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationTypeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EvaluatedUserProfileId = table.Column<int>(type: "integer", nullable: false),
                    EvaluatorUserProfileId = table.Column<int>(type: "integer", nullable: false),
                    CounterSingerUserProfileId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<decimal>(type: "numeric", nullable: false),
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
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_EvaluationStatusEnum_Status",
                        column: x => x.Status,
                        principalTable: "EvaluationStatusEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_EvaluationTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "EvaluationTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_UserProfiles_CounterSingerUserProfileId",
                        column: x => x.CounterSingerUserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_UserProfiles_EvaluatedUserProfileId",
                        column: x => x.EvaluatedUserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_UserProfiles_EvaluatorUserProfileId",
                        column: x => x.EvaluatorUserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EvaluationStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Draft" },
                    { 2, "Confirmed" },
                    { 3, "Accepted" },
                    { 4, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "EvaluationTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Execution" },
                    { 2, "Management" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_CounterSingerUserProfileId",
                table: "Evaluations",
                column: "CounterSingerUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EvaluatedUserProfileId",
                table: "Evaluations",
                column: "EvaluatedUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EvaluatorUserProfileId",
                table: "Evaluations",
                column: "EvaluatorUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Status",
                table: "Evaluations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Type",
                table: "Evaluations",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationStatusEnum_Name",
                table: "EvaluationStatusEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTypeEnum_Name",
                table: "EvaluationTypeEnum",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "EvaluationStatusEnum");

            migrationBuilder.DropTable(
                name: "EvaluationTypeEnum");
        }
    }
}
