using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Migrate_Registration_Flux_Tables_To_Contractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autobiographies_UserProfiles_UserProfileId",
                table: "Autobiographies");

            migrationBuilder.DropForeignKey(
                name: "FK_Bulletins_UserProfiles_UserProfileId",
                table: "Bulletins");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_NomenclatureRecords_BloodTypeId",
                table: "Contractors");

            migrationBuilder.DropForeignKey(
                name: "FK_KinshipRelationCriminalDatas_UserProfiles_UserProfileId",
                table: "KinshipRelationCriminalDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_KinshipRelations_UserProfiles_UserProfileId",
                table: "KinshipRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_KinshipRelationWithUserProfiles_UserProfiles_UserProfileId",
                table: "KinshipRelationWithUserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialStatuses_UserProfiles_UserProfileId",
                table: "MaterialStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_MilitaryObligations_UserProfiles_UserProfileId",
                table: "MilitaryObligations");

            migrationBuilder.DropForeignKey(
                name: "FK_ModernLanguageLevels_UserProfiles_UserProfileId",
                table: "ModernLanguageLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendationForStudies_UserProfiles_UserProfileId",
                table: "RecommendationForStudies");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationFluxSteps_UserProfiles_UserProfileId",
                table: "RegistrationFluxSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Studies_UserProfiles_UserProfileId",
                table: "Studies");

            migrationBuilder.DropTable(
                name: "UserProfileGeneralDatas");

            migrationBuilder.DropIndex(
                name: "IX_Studies_UserProfileId",
                table: "Studies");

            migrationBuilder.DropIndex(
                name: "IX_RegistrationFluxSteps_UserProfileId",
                table: "RegistrationFluxSteps");

            migrationBuilder.DropIndex(
                name: "IX_RecommendationForStudies_UserProfileId",
                table: "RecommendationForStudies");

            migrationBuilder.DropIndex(
                name: "IX_ModernLanguageLevels_UserProfileId",
                table: "ModernLanguageLevels");

            migrationBuilder.DropIndex(
                name: "IX_MilitaryObligations_UserProfileId",
                table: "MilitaryObligations");

            migrationBuilder.DropIndex(
                name: "IX_MaterialStatuses_UserProfileId",
                table: "MaterialStatuses");

            migrationBuilder.DropIndex(
                name: "IX_KinshipRelationWithUserProfiles_UserProfileId",
                table: "KinshipRelationWithUserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_KinshipRelations_UserProfileId",
                table: "KinshipRelations");

            migrationBuilder.DropIndex(
                name: "IX_KinshipRelationCriminalDatas_UserProfileId",
                table: "KinshipRelationCriminalDatas");

            migrationBuilder.DropIndex(
                name: "IX_Bulletins_UserProfileId",
                table: "Bulletins");

            migrationBuilder.DropIndex(
                name: "IX_Autobiographies_UserProfileId",
                table: "Autobiographies");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "RegistrationFluxSteps");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "RecommendationForStudies");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "ModernLanguageLevels");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "MilitaryObligations");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "MaterialStatuses");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "KinshipRelationWithUserProfiles");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "KinshipRelations");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "KinshipRelationCriminalDatas");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Bulletins");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Autobiographies");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Contractors",
                newName: "WorkPhone");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Contractors",
                newName: "HomePhone");

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "Studies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "RegistrationFluxSteps",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "RecommendationForStudies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "ModernLanguageLevels",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "MilitaryObligations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "MaterialStatuses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "KinshipRelationWithUserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "KinshipRelations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "KinshipRelationCriminalDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Sex",
                table: "Contractors",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "BloodTypeId",
                table: "Contractors",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CandidateCitizenshipId",
                table: "Contractors",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CandidateNationalityId",
                table: "Contractors",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateLanguageLevel",
                table: "Contractors",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "Bulletins",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "Autobiographies",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Studies_ContractorId",
                table: "Studies",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFluxSteps_ContractorId",
                table: "RegistrationFluxSteps",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationForStudies_ContractorId",
                table: "RecommendationForStudies",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModernLanguageLevels_ContractorId",
                table: "ModernLanguageLevels",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryObligations_ContractorId",
                table: "MilitaryObligations",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialStatuses_ContractorId",
                table: "MaterialStatuses",
                column: "ContractorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KinshipRelationWithUserProfiles_ContractorId",
                table: "KinshipRelationWithUserProfiles",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_KinshipRelations_ContractorId",
                table: "KinshipRelations",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_KinshipRelationCriminalDatas_ContractorId",
                table: "KinshipRelationCriminalDatas",
                column: "ContractorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_CandidateCitizenshipId",
                table: "Contractors",
                column: "CandidateCitizenshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_CandidateNationalityId",
                table: "Contractors",
                column: "CandidateNationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_StateLanguageLevel",
                table: "Contractors",
                column: "StateLanguageLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Bulletins_ContractorId",
                table: "Bulletins",
                column: "ContractorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Autobiographies_ContractorId",
                table: "Autobiographies",
                column: "ContractorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Autobiographies_Contractors_ContractorId",
                table: "Autobiographies",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bulletins_Contractors_ContractorId",
                table: "Bulletins",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_CandidateCitizens_CandidateCitizenshipId",
                table: "Contractors",
                column: "CandidateCitizenshipId",
                principalTable: "CandidateCitizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_CandidateNationalities_CandidateNationalityId",
                table: "Contractors",
                column: "CandidateNationalityId",
                principalTable: "CandidateNationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_NomenclatureRecords_BloodTypeId",
                table: "Contractors",
                column: "BloodTypeId",
                principalTable: "NomenclatureRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_StateLanguageLevel_StateLanguageLevel",
                table: "Contractors",
                column: "StateLanguageLevel",
                principalTable: "StateLanguageLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KinshipRelationCriminalDatas_Contractors_ContractorId",
                table: "KinshipRelationCriminalDatas",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KinshipRelations_Contractors_ContractorId",
                table: "KinshipRelations",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KinshipRelationWithUserProfiles_Contractors_ContractorId",
                table: "KinshipRelationWithUserProfiles",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialStatuses_Contractors_ContractorId",
                table: "MaterialStatuses",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MilitaryObligations_Contractors_ContractorId",
                table: "MilitaryObligations",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModernLanguageLevels_Contractors_ContractorId",
                table: "ModernLanguageLevels",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendationForStudies_Contractors_ContractorId",
                table: "RecommendationForStudies",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationFluxSteps_Contractors_ContractorId",
                table: "RegistrationFluxSteps",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_Contractors_ContractorId",
                table: "Studies",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autobiographies_Contractors_ContractorId",
                table: "Autobiographies");

            migrationBuilder.DropForeignKey(
                name: "FK_Bulletins_Contractors_ContractorId",
                table: "Bulletins");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_CandidateCitizens_CandidateCitizenshipId",
                table: "Contractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_CandidateNationalities_CandidateNationalityId",
                table: "Contractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_NomenclatureRecords_BloodTypeId",
                table: "Contractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_StateLanguageLevel_StateLanguageLevel",
                table: "Contractors");

            migrationBuilder.DropForeignKey(
                name: "FK_KinshipRelationCriminalDatas_Contractors_ContractorId",
                table: "KinshipRelationCriminalDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_KinshipRelations_Contractors_ContractorId",
                table: "KinshipRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_KinshipRelationWithUserProfiles_Contractors_ContractorId",
                table: "KinshipRelationWithUserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialStatuses_Contractors_ContractorId",
                table: "MaterialStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_MilitaryObligations_Contractors_ContractorId",
                table: "MilitaryObligations");

            migrationBuilder.DropForeignKey(
                name: "FK_ModernLanguageLevels_Contractors_ContractorId",
                table: "ModernLanguageLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendationForStudies_Contractors_ContractorId",
                table: "RecommendationForStudies");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationFluxSteps_Contractors_ContractorId",
                table: "RegistrationFluxSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Studies_Contractors_ContractorId",
                table: "Studies");

            migrationBuilder.DropIndex(
                name: "IX_Studies_ContractorId",
                table: "Studies");

            migrationBuilder.DropIndex(
                name: "IX_RegistrationFluxSteps_ContractorId",
                table: "RegistrationFluxSteps");

            migrationBuilder.DropIndex(
                name: "IX_RecommendationForStudies_ContractorId",
                table: "RecommendationForStudies");

            migrationBuilder.DropIndex(
                name: "IX_ModernLanguageLevels_ContractorId",
                table: "ModernLanguageLevels");

            migrationBuilder.DropIndex(
                name: "IX_MilitaryObligations_ContractorId",
                table: "MilitaryObligations");

            migrationBuilder.DropIndex(
                name: "IX_MaterialStatuses_ContractorId",
                table: "MaterialStatuses");

            migrationBuilder.DropIndex(
                name: "IX_KinshipRelationWithUserProfiles_ContractorId",
                table: "KinshipRelationWithUserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_KinshipRelations_ContractorId",
                table: "KinshipRelations");

            migrationBuilder.DropIndex(
                name: "IX_KinshipRelationCriminalDatas_ContractorId",
                table: "KinshipRelationCriminalDatas");

            migrationBuilder.DropIndex(
                name: "IX_Contractors_CandidateCitizenshipId",
                table: "Contractors");

            migrationBuilder.DropIndex(
                name: "IX_Contractors_CandidateNationalityId",
                table: "Contractors");

            migrationBuilder.DropIndex(
                name: "IX_Contractors_StateLanguageLevel",
                table: "Contractors");

            migrationBuilder.DropIndex(
                name: "IX_Bulletins_ContractorId",
                table: "Bulletins");

            migrationBuilder.DropIndex(
                name: "IX_Autobiographies_ContractorId",
                table: "Autobiographies");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "RegistrationFluxSteps");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "RecommendationForStudies");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "ModernLanguageLevels");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "MilitaryObligations");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "MaterialStatuses");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "KinshipRelationWithUserProfiles");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "KinshipRelations");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "KinshipRelationCriminalDatas");

            migrationBuilder.DropColumn(
                name: "CandidateCitizenshipId",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "CandidateNationalityId",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "StateLanguageLevel",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "Bulletins");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "Autobiographies");

            migrationBuilder.RenameColumn(
                name: "WorkPhone",
                table: "Contractors",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "HomePhone",
                table: "Contractors",
                newName: "FirstName");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Studies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "RegistrationFluxSteps",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "RecommendationForStudies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "ModernLanguageLevels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "MilitaryObligations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "MaterialStatuses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "KinshipRelationWithUserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "KinshipRelations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "KinshipRelationCriminalDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Sex",
                table: "Contractors",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BloodTypeId",
                table: "Contractors",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Contractors",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Contractors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Bulletins",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Autobiographies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserProfileGeneralDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CandidateCitizenshipId = table.Column<int>(type: "integer", nullable: false),
                    CandidateNationalityId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    HomePhone = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Sex = table.Column<int>(type: "integer", nullable: false),
                    StateLanguageLevel = table.Column<int>(type: "integer", nullable: false),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    WorkPhone = table.Column<string>(type: "text", nullable: true)
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
                name: "IX_Studies_UserProfileId",
                table: "Studies",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationFluxSteps_UserProfileId",
                table: "RegistrationFluxSteps",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationForStudies_UserProfileId",
                table: "RecommendationForStudies",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ModernLanguageLevels_UserProfileId",
                table: "ModernLanguageLevels",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryObligations_UserProfileId",
                table: "MilitaryObligations",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialStatuses_UserProfileId",
                table: "MaterialStatuses",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KinshipRelationWithUserProfiles_UserProfileId",
                table: "KinshipRelationWithUserProfiles",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_KinshipRelations_UserProfileId",
                table: "KinshipRelations",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_KinshipRelationCriminalDatas_UserProfileId",
                table: "KinshipRelationCriminalDatas",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bulletins_UserProfileId",
                table: "Bulletins",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Autobiographies_UserProfileId",
                table: "Autobiographies",
                column: "UserProfileId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Autobiographies_UserProfiles_UserProfileId",
                table: "Autobiographies",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bulletins_UserProfiles_UserProfileId",
                table: "Bulletins",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_NomenclatureRecords_BloodTypeId",
                table: "Contractors",
                column: "BloodTypeId",
                principalTable: "NomenclatureRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KinshipRelationCriminalDatas_UserProfiles_UserProfileId",
                table: "KinshipRelationCriminalDatas",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KinshipRelations_UserProfiles_UserProfileId",
                table: "KinshipRelations",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KinshipRelationWithUserProfiles_UserProfiles_UserProfileId",
                table: "KinshipRelationWithUserProfiles",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialStatuses_UserProfiles_UserProfileId",
                table: "MaterialStatuses",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MilitaryObligations_UserProfiles_UserProfileId",
                table: "MilitaryObligations",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModernLanguageLevels_UserProfiles_UserProfileId",
                table: "ModernLanguageLevels",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendationForStudies_UserProfiles_UserProfileId",
                table: "RecommendationForStudies",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationFluxSteps_UserProfiles_UserProfileId",
                table: "RegistrationFluxSteps",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_UserProfiles_UserProfileId",
                table: "Studies",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
