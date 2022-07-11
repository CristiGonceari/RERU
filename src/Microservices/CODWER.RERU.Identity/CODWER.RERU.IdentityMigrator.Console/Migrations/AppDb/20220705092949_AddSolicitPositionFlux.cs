using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class AddSolicitPositionFlux : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitedVacantPositions_Events_EventId",
                table: "SolicitedVacantPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitedVacantPositions_SolicitedTestStatusEnum_SolicitedT~",
                table: "SolicitedVacantPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitedVacantPositions_TestTemplates_TestTemplateId",
                table: "SolicitedVacantPositions");

            migrationBuilder.DropTable(
                name: "SolicitedTestStatusEnum");

            migrationBuilder.DropIndex(
                name: "IX_SolicitedVacantPositions_EventId",
                table: "SolicitedVacantPositions");

            migrationBuilder.DropIndex(
                name: "IX_SolicitedVacantPositions_SolicitedTestStatus",
                table: "SolicitedVacantPositions");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "SolicitedVacantPositions");

            migrationBuilder.DropColumn(
                name: "SolicitedTestStatus",
                table: "SolicitedVacantPositions");

            migrationBuilder.DropColumn(
                name: "SolicitedTime",
                table: "SolicitedVacantPositions");

            migrationBuilder.RenameColumn(
                name: "TestTemplateId",
                table: "SolicitedVacantPositions",
                newName: "SolicitedPositionStatus");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitedVacantPositions_TestTemplateId",
                table: "SolicitedVacantPositions",
                newName: "IX_SolicitedVacantPositions_SolicitedPositionStatus");

            migrationBuilder.CreateTable(
                name: "SolicitedPositionStatusEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitedPositionStatusEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitedVacantPositionEmailMessageEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitedVacantPositionEmailMessageEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitedVacantPositionUserFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    FileId = table.Column<string>(type: "text", nullable: true),
                    SolicitedVacantPositionId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_SolicitedVacantPositionUserFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitedVacantPositionUserFiles_RequiredDocuments_Required~",
                        column: x => x.RequiredDocumentId,
                        principalTable: "RequiredDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitedVacantPositionUserFiles_SolicitedVacantPositions_S~",
                        column: x => x.SolicitedVacantPositionId,
                        principalTable: "SolicitedVacantPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitedVacantPositionUserFiles_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitedVacantPositionEmailMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: true),
                    MessageType = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitedVacantPositionEmailMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitedVacantPositionEmailMessages_SolicitedVacantPositio~",
                        column: x => x.MessageType,
                        principalTable: "SolicitedVacantPositionEmailMessageEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "SolicitedPositionStatusEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "New" },
                    { 1, "Refused" },
                    { 2, "Approved" },
                    { 3, "Wait" }
                });

            migrationBuilder.InsertData(
                table: "SolicitedVacantPositionEmailMessageEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Approve" },
                    { 1, "Reject" },
                    { 2, "Waiting" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedPositionStatusEnum_Name",
                table: "SolicitedPositionStatusEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedVacantPositionEmailMessageEnum_Name",
                table: "SolicitedVacantPositionEmailMessageEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedVacantPositionEmailMessages_MessageType",
                table: "SolicitedVacantPositionEmailMessages",
                column: "MessageType");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedVacantPositionUserFiles_RequiredDocumentId",
                table: "SolicitedVacantPositionUserFiles",
                column: "RequiredDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedVacantPositionUserFiles_SolicitedVacantPositionId",
                table: "SolicitedVacantPositionUserFiles",
                column: "SolicitedVacantPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedVacantPositionUserFiles_UserProfileId",
                table: "SolicitedVacantPositionUserFiles",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitedVacantPositions_SolicitedPositionStatusEnum_Solici~",
                table: "SolicitedVacantPositions",
                column: "SolicitedPositionStatus",
                principalTable: "SolicitedPositionStatusEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitedVacantPositions_SolicitedPositionStatusEnum_Solici~",
                table: "SolicitedVacantPositions");

            migrationBuilder.DropTable(
                name: "SolicitedPositionStatusEnum");

            migrationBuilder.DropTable(
                name: "SolicitedVacantPositionEmailMessages");

            migrationBuilder.DropTable(
                name: "SolicitedVacantPositionUserFiles");

            migrationBuilder.DropTable(
                name: "SolicitedVacantPositionEmailMessageEnum");

            migrationBuilder.RenameColumn(
                name: "SolicitedPositionStatus",
                table: "SolicitedVacantPositions",
                newName: "TestTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitedVacantPositions_SolicitedPositionStatus",
                table: "SolicitedVacantPositions",
                newName: "IX_SolicitedVacantPositions_TestTemplateId");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "SolicitedVacantPositions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SolicitedTestStatus",
                table: "SolicitedVacantPositions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SolicitedTime",
                table: "SolicitedVacantPositions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                name: "IX_SolicitedVacantPositions_EventId",
                table: "SolicitedVacantPositions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedVacantPositions_SolicitedTestStatus",
                table: "SolicitedVacantPositions",
                column: "SolicitedTestStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitedTestStatusEnum_Name",
                table: "SolicitedTestStatusEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitedVacantPositions_Events_EventId",
                table: "SolicitedVacantPositions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitedVacantPositions_SolicitedTestStatusEnum_SolicitedT~",
                table: "SolicitedVacantPositions",
                column: "SolicitedTestStatus",
                principalTable: "SolicitedTestStatusEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitedVacantPositions_TestTemplates_TestTemplateId",
                table: "SolicitedVacantPositions",
                column: "TestTemplateId",
                principalTable: "TestTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
