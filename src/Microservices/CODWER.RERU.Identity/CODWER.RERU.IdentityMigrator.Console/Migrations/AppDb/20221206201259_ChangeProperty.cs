using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class ChangeProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DecisionCounterSigner",
                table: "Evaluations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalScore",
                table: "Evaluations",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FunctionSubdivision",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubdivisionEvaluated",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DecisionCounterSignerEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecisionCounterSignerEnum", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DecisionCounterSignerEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Accept" },
                    { 2, "Reevaluation" }
                });

            migrationBuilder.UpdateData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Soldat");

            migrationBuilder.UpdateData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Caporal");

            migrationBuilder.InsertData(
                table: "GradeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "SergentMajor" },
                    { 6, "Plutonier" },
                    { 7, "PlutonierMajor" },
                    { 8, "PlutonierAdjutant" },
                    { 9, "Locotenent" },
                    { 3, "SergentInferior" },
                    { 11, "Capitan" },
                    { 12, "Maior" },
                    { 13, "LocotenentColonel" },
                    { 14, "Colonel" },
                    { 15, "GeneralDeBrigada" },
                    { 16, "GeneralDeDivizie" },
                    { 17, "GeneralDeCorpArmata" },
                    { 4, "Sergent" },
                    { 10, "LocotenentMajor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_DecisionCounterSigner",
                table: "Evaluations",
                column: "DecisionCounterSigner");

            migrationBuilder.CreateIndex(
                name: "IX_DecisionCounterSignerEnum_Name",
                table: "DecisionCounterSignerEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_DecisionCounterSignerEnum_DecisionCounterSigner",
                table: "Evaluations",
                column: "DecisionCounterSigner",
                principalTable: "DecisionCounterSignerEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_DecisionCounterSignerEnum_DecisionCounterSigner",
                table: "Evaluations");

            migrationBuilder.DropTable(
                name: "DecisionCounterSignerEnum");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_DecisionCounterSigner",
                table: "Evaluations");

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DropColumn(
                name: "DecisionCounterSigner",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FinalScore",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "FunctionSubdivision",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SubdivisionEvaluated",
                table: "Evaluations");

            migrationBuilder.UpdateData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Special");

            migrationBuilder.UpdateData(
                table: "GradeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Military");
        }
    }
}
