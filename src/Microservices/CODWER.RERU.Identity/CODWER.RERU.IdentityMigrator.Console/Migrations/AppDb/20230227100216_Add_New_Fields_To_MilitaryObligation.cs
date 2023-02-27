using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Add_New_Fields_To_MilitaryObligation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilitaryObligations_MilitaryObligationTypeEnum_MilitaryObli~",
                table: "MilitaryObligations");

            migrationBuilder.AlterColumn<int>(
                name: "MilitaryBookletNumber",
                table: "MilitaryObligations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndObligationPeriod",
                table: "MilitaryObligations",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstitutionAdress",
                table: "MilitaryObligations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstitutionName",
                table: "MilitaryObligations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartObligationPeriod",
                table: "MilitaryObligations",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MilitaryObligations_MilitaryObligationTypeEnum_MilitaryObli~",
                table: "MilitaryObligations",
                column: "MilitaryObligationType",
                principalTable: "MilitaryObligationTypeEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilitaryObligations_MilitaryObligationTypeEnum_MilitaryObli~",
                table: "MilitaryObligations");

            migrationBuilder.DropColumn(
                name: "EndObligationPeriod",
                table: "MilitaryObligations");

            migrationBuilder.DropColumn(
                name: "InstitutionAdress",
                table: "MilitaryObligations");

            migrationBuilder.DropColumn(
                name: "InstitutionName",
                table: "MilitaryObligations");

            migrationBuilder.DropColumn(
                name: "StartObligationPeriod",
                table: "MilitaryObligations");

            migrationBuilder.AlterColumn<int>(
                name: "MilitaryBookletNumber",
                table: "MilitaryObligations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MilitaryObligations_MilitaryObligationTypeEnum_MilitaryObli~",
                table: "MilitaryObligations",
                column: "MilitaryObligationType",
                principalTable: "MilitaryObligationTypeEnum",
                principalColumn: "Id");
        }
    }
}
