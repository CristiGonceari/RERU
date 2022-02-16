using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.Core.Data.Persistence.Migrations
{
    public partial class First_PostGreSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentBodies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentBodies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    DocumentStorageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleTypeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleTypeEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypeEnum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    FatherName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Idnp = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    AvatarId = table.Column<int>(type: "integer", nullable: true),
                    TokenLifetime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RequiresDataEntry = table.Column<bool>(type: "boolean", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Documents_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PublicUrl = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    InternalGatewayAPIPath = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_ModuleStatus_Status",
                        column: x => x.Status,
                        principalTable: "ModuleStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Modules_ModuleTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "ModuleTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileIdentity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Identificator = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileIdentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileIdentity_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModulePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModulePermissions_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    IsAssignByDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleRoles_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleRoles_RoleTypeEnum_Type",
                        column: x => x.Type,
                        principalTable: "RoleTypeEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModuleRolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleRoleId = table.Column<int>(type: "integer", nullable: false),
                    ModulePermissionId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleRolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleRolePermissions_ModulePermissions_ModulePermissionId",
                        column: x => x.ModulePermissionId,
                        principalTable: "ModulePermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleRolePermissions_ModuleRoles_ModuleRoleId",
                        column: x => x.ModuleRoleId,
                        principalTable: "ModuleRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileModuleRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    ModuleRoleId = table.Column<int>(type: "integer", nullable: false),
                    CreateById = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileModuleRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileModuleRoles_ModuleRoles_ModuleRoleId",
                        column: x => x.ModuleRoleId,
                        principalTable: "ModuleRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfileModuleRoles_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ModuleStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Offline" },
                    { 1, "Online" }
                });

            migrationBuilder.InsertData(
                table: "ModuleTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Dynamic" },
                    { 1, "Default" }
                });

            migrationBuilder.InsertData(
                table: "RoleTypeEnum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Default" },
                    { 2, "Dynamic" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModulePermissions_ModuleId",
                table: "ModulePermissions",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleRolePermissions_ModulePermissionId",
                table: "ModuleRolePermissions",
                column: "ModulePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleRolePermissions_ModuleRoleId",
                table: "ModuleRolePermissions",
                column: "ModuleRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleRoles_ModuleId",
                table: "ModuleRoles",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleRoles_Type",
                table: "ModuleRoles",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Status",
                table: "Modules",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Type",
                table: "Modules",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleStatus_Name",
                table: "ModuleStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleTypeEnum_Name",
                table: "ModuleTypeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleTypeEnum_Name",
                table: "RoleTypeEnum",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileIdentity_UserProfileId",
                table: "UserProfileIdentity",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileModuleRoles_ModuleRoleId",
                table: "UserProfileModuleRoles",
                column: "ModuleRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileModuleRoles_UserProfileId",
                table: "UserProfileModuleRoles",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AvatarId",
                table: "UserProfiles",
                column: "AvatarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentBodies");

            migrationBuilder.DropTable(
                name: "ModuleRolePermissions");

            migrationBuilder.DropTable(
                name: "UserProfileIdentity");

            migrationBuilder.DropTable(
                name: "UserProfileModuleRoles");

            migrationBuilder.DropTable(
                name: "ModulePermissions");

            migrationBuilder.DropTable(
                name: "ModuleRoles");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "RoleTypeEnum");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "ModuleStatus");

            migrationBuilder.DropTable(
                name: "ModuleTypeEnum");
        }
    }
}
