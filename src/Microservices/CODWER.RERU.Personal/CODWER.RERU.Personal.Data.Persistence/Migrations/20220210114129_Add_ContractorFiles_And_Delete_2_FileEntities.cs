using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CODWER.RERU.Personal.Data.Persistence.Migrations
{
    public partial class Add_ContractorFiles_And_Delete_2_FileEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ByteFiles_FileId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_DismissalRequests_ByteFiles_OrderId",
                table: "DismissalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DismissalRequests_ByteFiles_RequestId",
                table: "DismissalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_ByteFiles_OrderId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_ByteFiles_VacationOrderId",
                table: "Vacations");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_ByteFiles_VacationRequestId",
                table: "Vacations");

            migrationBuilder.DropTable(
                name: "FileSignatures");

            migrationBuilder.DropTable(
                name: "ByteFiles");

            migrationBuilder.DropIndex(
                name: "IX_Vacations_VacationOrderId",
                table: "Vacations");

            migrationBuilder.DropIndex(
                name: "IX_Vacations_VacationRequestId",
                table: "Vacations");

            migrationBuilder.DropIndex(
                name: "IX_Positions_OrderId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_DismissalRequests_OrderId",
                table: "DismissalRequests");

            migrationBuilder.DropIndex(
                name: "IX_DismissalRequests_RequestId",
                table: "DismissalRequests");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_FileId",
                table: "Contracts");

            migrationBuilder.AlterColumn<string>(
                name: "VacationRequestId",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VacationOrderId",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "Positions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestId",
                table: "DismissalRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "DismissalRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileId",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ContractorFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractorId = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorFiles_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Question");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Test");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Media");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Documents");

            migrationBuilder.InsertData(
                table: "FileTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "TestTemplate" },
                    { 6, "IdentityFiles" },
                    { 7, "Photos" },
                    { 8, "Request" },
                    { 9, "Order" },
                    { 10, "Cim" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorFiles_ContractorId",
                table: "ContractorFiles",
                column: "ContractorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorFiles");

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AlterColumn<int>(
                name: "VacationRequestId",
                table: "Vacations",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VacationOrderId",
                table: "Vacations",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Positions",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "DismissalRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "DismissalRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Contracts",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ByteFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorId = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsSigned = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UniqueFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ByteFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ByteFiles_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ByteFiles_FileTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "FileTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileSignatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractorId = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdateById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSignatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileSignatures_ByteFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "ByteFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileSignatures_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Identity");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Request");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Order");

            migrationBuilder.UpdateData(
                table: "FileTypeEnum",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Cim");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_VacationOrderId",
                table: "Vacations",
                column: "VacationOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_VacationRequestId",
                table: "Vacations",
                column: "VacationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_OrderId",
                table: "Positions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DismissalRequests_OrderId",
                table: "DismissalRequests",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DismissalRequests_RequestId",
                table: "DismissalRequests",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_FileId",
                table: "Contracts",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ByteFiles_ContractorId",
                table: "ByteFiles",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ByteFiles_Type",
                table: "ByteFiles",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_FileSignatures_ContractorId",
                table: "FileSignatures",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_FileSignatures_FileId",
                table: "FileSignatures",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ByteFiles_FileId",
                table: "Contracts",
                column: "FileId",
                principalTable: "ByteFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DismissalRequests_ByteFiles_OrderId",
                table: "DismissalRequests",
                column: "OrderId",
                principalTable: "ByteFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DismissalRequests_ByteFiles_RequestId",
                table: "DismissalRequests",
                column: "RequestId",
                principalTable: "ByteFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_ByteFiles_OrderId",
                table: "Positions",
                column: "OrderId",
                principalTable: "ByteFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_ByteFiles_VacationOrderId",
                table: "Vacations",
                column: "VacationOrderId",
                principalTable: "ByteFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_ByteFiles_VacationRequestId",
                table: "Vacations",
                column: "VacationRequestId",
                principalTable: "ByteFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
