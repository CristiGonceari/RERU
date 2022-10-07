using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class AddMedicalColumnEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalColumn",
                table: "CandidatePositions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicalColumnEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalColumnEnum", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MedicalColumnEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Column1" },
                    { 1, "Column2" },
                    { 2, "Column3" },
                    { 3, "Column4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidatePositions_MedicalColumn",
                table: "CandidatePositions",
                column: "MedicalColumn");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalColumnEnum_Name",
                table: "MedicalColumnEnum",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidatePositions_MedicalColumnEnum_MedicalColumn",
                table: "CandidatePositions",
                column: "MedicalColumn",
                principalTable: "MedicalColumnEnum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidatePositions_MedicalColumnEnum_MedicalColumn",
                table: "CandidatePositions");

            migrationBuilder.DropTable(
                name: "MedicalColumnEnum");

            migrationBuilder.DropIndex(
                name: "IX_CandidatePositions_MedicalColumn",
                table: "CandidatePositions");

            migrationBuilder.DropColumn(
                name: "MedicalColumn",
                table: "CandidatePositions");
        }
    }
}
