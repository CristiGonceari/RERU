using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Added_SanctionEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SanctionAppliedEvaluationCourse",
                table: "Evaluations");

            migrationBuilder.AddColumn<int>(
                name: "SanctionApplied",
                table: "Evaluations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SanctionEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanctionEnum", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SanctionEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Nu_sunt" },
                    { 2, "Avertisment" },
                    { 3, "Mustrare" },
                    { 4, "Mustrare_aspră" },
                    { 5, "Retrogradare_cu_un_grad_special" },
                    { 6, "Retrogradare_în_funcţie" },
                    { 7, "Concediere_din_funcţia_publică_cu_statut_special" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_SanctionApplied",
                table: "Evaluations",
                column: "SanctionApplied");

            migrationBuilder.CreateIndex(
                name: "IX_SanctionEnum_Name",
                table: "SanctionEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_SanctionEnum_SanctionApplied",
                table: "Evaluations",
                column: "SanctionApplied",
                principalTable: "SanctionEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_SanctionEnum_SanctionApplied",
                table: "Evaluations");

            migrationBuilder.DropTable(
                name: "SanctionEnum");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_SanctionApplied",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SanctionApplied",
                table: "Evaluations");

            migrationBuilder.AddColumn<string>(
                name: "SanctionAppliedEvaluationCourse",
                table: "Evaluations",
                type: "text",
                nullable: true);
        }
    }
}
