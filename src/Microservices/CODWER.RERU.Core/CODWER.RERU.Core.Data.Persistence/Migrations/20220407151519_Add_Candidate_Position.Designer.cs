﻿// <auto-generated />
using System;
using CODWER.RERU.Core.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CODWER.RERU.Core.Data.Persistence.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    [Migration("20220407151519_Add_Candidate_Position")]
    partial class Add_Candidate_Position
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("CreateById")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.CandidatePosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CreateById")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("CandidatePositions");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<string>("CreateById")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<string>("InternalGatewayAPIPath")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<string>("PublicUrl")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Status");

                    b.HasIndex("Type");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.ModulePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("CreateById")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("ModuleId")
                        .HasColumnType("integer");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("ModulePermissions");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.ModuleRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("CreateById")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsAssignByDefault")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("ModuleId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("Type");

                    b.ToTable("ModuleRoles");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.ModuleRolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CreateById")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ModulePermissionId")
                        .HasColumnType("integer");

                    b.Property<int>("ModuleRoleId")
                        .HasColumnType("integer");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ModulePermissionId");

                    b.HasIndex("ModuleRoleId");

                    b.ToTable("ModuleRolePermissions");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("CandidatePositionId")
                        .HasColumnType("integer");

                    b.Property<string>("CreateById")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FatherName")
                        .HasColumnType("text");

                    b.Property<string>("Idnp")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("MediaFileId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("RequiresDataEntry")
                        .HasColumnType("boolean");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<DateTime?>("TokenLifetime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CandidatePositionId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.UserProfileIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CreateById")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Identificator")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserProfileIdentity");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.UserProfileModuleRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CreateById")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ModuleRoleId")
                        .HasColumnType("integer");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ModuleRoleId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserProfileModuleRoles");
                });

            modelBuilder.Entity("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.ModuleStatus>", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ModuleStatus");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Offline"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Online"
                        });
                });

            modelBuilder.Entity("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.ModuleTypeEnum>", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ModuleTypeEnum");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Dynamic"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Default"
                        });
                });

            modelBuilder.Entity("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.RoleTypeEnum>", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("RoleTypeEnum");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Default"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Dynamic"
                        });
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.Module", b =>
                {
                    b.HasOne("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.ModuleStatus>", null)
                        .WithMany()
                        .HasForeignKey("Status")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.ModuleTypeEnum>", null)
                        .WithMany()
                        .HasForeignKey("Type")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.ModulePermission", b =>
                {
                    b.HasOne("CODWER.RERU.Core.Data.Entities.Module", null)
                        .WithMany("Permissions")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.ModuleRole", b =>
                {
                    b.HasOne("CODWER.RERU.Core.Data.Entities.Module", "Module")
                        .WithMany("Roles")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.RoleTypeEnum>", null)
                        .WithMany()
                        .HasForeignKey("Type")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.ModuleRolePermission", b =>
                {
                    b.HasOne("CODWER.RERU.Core.Data.Entities.ModulePermission", "Permission")
                        .WithMany()
                        .HasForeignKey("ModulePermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CODWER.RERU.Core.Data.Entities.ModuleRole", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("ModuleRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.UserProfile", b =>
                {
                    b.HasOne("CODWER.RERU.Core.Data.Entities.CandidatePosition", "CandidatePosition")
                        .WithMany("UserProfiles")
                        .HasForeignKey("CandidatePositionId");

                    b.Navigation("CandidatePosition");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.UserProfileIdentity", b =>
                {
                    b.HasOne("CODWER.RERU.Core.Data.Entities.UserProfile", null)
                        .WithMany("Identities")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.UserProfileModuleRole", b =>
                {
                    b.HasOne("CODWER.RERU.Core.Data.Entities.ModuleRole", "ModuleRole")
                        .WithMany()
                        .HasForeignKey("ModuleRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CODWER.RERU.Core.Data.Entities.UserProfile", "UserProfile")
                        .WithMany("ModuleRoles")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModuleRole");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.ModuleStatus>", b =>
                {
                    b.HasOne("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.ModuleStatus>", null)
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.ModuleTypeEnum>", b =>
                {
                    b.HasOne("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.ModuleTypeEnum>", null)
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.RoleTypeEnum>", b =>
                {
                    b.HasOne("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<CODWER.RERU.Core.Data.Entities.Enums.RoleTypeEnum>", null)
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.CandidatePosition", b =>
                {
                    b.Navigation("UserProfiles");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.Module", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.ModuleRole", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("CODWER.RERU.Core.Data.Entities.UserProfile", b =>
                {
                    b.Navigation("Identities");

                    b.Navigation("ModuleRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
