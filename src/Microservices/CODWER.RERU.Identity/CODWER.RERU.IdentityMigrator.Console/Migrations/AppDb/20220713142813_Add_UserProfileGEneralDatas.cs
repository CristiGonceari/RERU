using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_UserProfileGEneralDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "CandidateCitizenshipId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CandidateNationalityId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "HomePhone",
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

            migrationBuilder.CreateTable(
                name: "UserProfileGeneralDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    WorkPhone = table.Column<string>(type: "text", nullable: true),
                    HomePhone = table.Column<string>(type: "text", nullable: true),
                    Sex = table.Column<int>(type: "integer", nullable: false),
                    StateLanguageLevel = table.Column<int>(type: "integer", nullable: false),
                    CandidateNationalityId = table.Column<int>(type: "integer", nullable: false),
                    CandidateCitizenshipId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileGeneralDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileGeneralDatas_CandidateCitizens_CandidateCitizens~",
                        column: x => x.CandidateCitizenshipId,
                        principalTable: "CandidateCitizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfileGeneralDatas_CandidateNationalities_CandidateNat~",
                        column: x => x.CandidateNationalityId,
                        principalTable: "CandidateNationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfileGeneralDatas_SexTypeEnum_Sex",
                        column: x => x.Sex,
                        principalTable: "SexTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfileGeneralDatas_StateLanguageLevel_StateLanguageLev~",
                        column: x => x.StateLanguageLevel,
                        principalTable: "StateLanguageLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfileGeneralDatas_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileGeneralDatas_CandidateCitizenshipId",
                table: "UserProfileGeneralDatas",
                column: "CandidateCitizenshipId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileGeneralDatas_CandidateNationalityId",
                table: "UserProfileGeneralDatas",
                column: "CandidateNationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileGeneralDatas_Sex",
                table: "UserProfileGeneralDatas",
                column: "Sex");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileGeneralDatas_StateLanguageLevel",
                table: "UserProfileGeneralDatas",
                column: "StateLanguageLevel");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileGeneralDatas_UserProfileId",
                table: "UserProfileGeneralDatas",
                column: "UserProfileId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfileGeneralDatas");

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
    }
}
