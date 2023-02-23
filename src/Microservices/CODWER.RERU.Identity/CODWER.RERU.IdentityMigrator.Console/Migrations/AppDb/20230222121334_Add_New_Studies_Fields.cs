using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_New_Studies_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studies_StudyTypes_StudyTypeId",
                table: "Studies");

            migrationBuilder.DeleteData(
                table: "MilitaryObligationTypeEnum",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "ValidationId",
                table: "StudyTypes",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudyTypeId",
                table: "Studies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CreditCount",
                table: "Studies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndStudyPeriod",
                table: "Studies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "Studies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartStudyPeriod",
                table: "Studies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudyActNumber",
                table: "Studies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StudyActRelaseDay",
                table: "Studies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudyActSeries",
                table: "Studies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudyCourse",
                table: "Studies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudyProfile",
                table: "Studies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Studies",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudyCourseType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyCourseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyProfileEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyProfileEnum", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "StudyCourseType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Initial" },
                    { 2, "Perfecting" },
                    { 3, "Specialization" },
                    { 4, "Retraining" }
                });

            migrationBuilder.InsertData(
                table: "StudyProfileEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Real" },
                    { 2, "Humanist" },
                    { 3, "Arts" },
                    { 4, "Sport" },
                    { 5, "Etc" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Studies_StudyCourse",
                table: "Studies",
                column: "StudyCourse");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_StudyProfile",
                table: "Studies",
                column: "StudyProfile");

            migrationBuilder.CreateIndex(
                name: "IX_StudyCourseType_Name",
                table: "StudyCourseType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudyProfileEnum_Name",
                table: "StudyProfileEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_StudyCourseType_StudyCourse",
                table: "Studies",
                column: "StudyCourse",
                principalTable: "StudyCourseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_StudyProfileEnum_StudyProfile",
                table: "Studies",
                column: "StudyProfile",
                principalTable: "StudyProfileEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_StudyTypes_StudyTypeId",
                table: "Studies",
                column: "StudyTypeId",
                principalTable: "StudyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studies_StudyCourseType_StudyCourse",
                table: "Studies");

            migrationBuilder.DropForeignKey(
                name: "FK_Studies_StudyProfileEnum_StudyProfile",
                table: "Studies");

            migrationBuilder.DropForeignKey(
                name: "FK_Studies_StudyTypes_StudyTypeId",
                table: "Studies");

            migrationBuilder.DropTable(
                name: "StudyCourseType");

            migrationBuilder.DropTable(
                name: "StudyProfileEnum");

            migrationBuilder.DropIndex(
                name: "IX_Studies_StudyCourse",
                table: "Studies");

            migrationBuilder.DropIndex(
                name: "IX_Studies_StudyProfile",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "ValidationId",
                table: "StudyTypes");

            migrationBuilder.DropColumn(
                name: "CreditCount",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "EndStudyPeriod",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "StartStudyPeriod",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "StudyActNumber",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "StudyActRelaseDay",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "StudyActSeries",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "StudyCourse",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "StudyProfile",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Studies");

            migrationBuilder.AlterColumn<int>(
                name: "StudyTypeId",
                table: "Studies",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "MilitaryObligationTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Reservist" });

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_StudyTypes_StudyTypeId",
                table: "Studies",
                column: "StudyTypeId",
                principalTable: "StudyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
