using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Evaluation.Data.Persistence.Migrations
{
    public partial class Add_PdfFileId_TestType_QuetionUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PdfFileId",
                table: "TestTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PdfFileId",
                table: "QuestionUnits",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfFileId",
                table: "TestTypes");

            migrationBuilder.DropColumn(
                name: "PdfFileId",
                table: "QuestionUnits");
        }
    }
}
