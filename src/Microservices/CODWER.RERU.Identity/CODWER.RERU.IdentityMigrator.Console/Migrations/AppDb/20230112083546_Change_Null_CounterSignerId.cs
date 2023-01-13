using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.IdentityMigrator.Console.Migrations.AppDb
{
    public partial class Change_Null_CounterSignerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_UserProfiles_CounterSignerUserProfileId",
                table: "Evaluations");

            migrationBuilder.AlterColumn<int>(
                name: "CounterSignerUserProfileId",
                table: "Evaluations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Schiță");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Confirmată");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Acceptată");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Respinsă");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Contrasemnată");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Respinsă_contrasemnatar");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Finisată");

            migrationBuilder.UpdateData(
                table: "EvaluationTypeEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Execuție");

            migrationBuilder.UpdateData(
                table: "EvaluationTypeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Conducere");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_UserProfiles_CounterSignerUserProfileId",
                table: "Evaluations",
                column: "CounterSignerUserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_UserProfiles_CounterSignerUserProfileId",
                table: "Evaluations");

            migrationBuilder.AlterColumn<int>(
                name: "CounterSignerUserProfileId",
                table: "Evaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Draft");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Confirmed");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Accepted");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Rejected");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "CounterSignAccept");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "CounterSignReject");

            migrationBuilder.UpdateData(
                table: "EvaluationStatusEnum",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Finished");

            migrationBuilder.UpdateData(
                table: "EvaluationTypeEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Execution");

            migrationBuilder.UpdateData(
                table: "EvaluationTypeEnum",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Management");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_UserProfiles_CounterSignerUserProfileId",
                table: "Evaluations",
                column: "CounterSignerUserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
