using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.Configurations;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Entities.Documents;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Entities.IdentityDocuments;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords.RecordValues;
using CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations;
using CODWER.RERU.Personal.Data.Entities.Studies;
using CODWER.RERU.Personal.Data.Entities.TimeSheetTables;
using CODWER.RERU.Personal.Data.Entities.User;
using CVU.ERP.Common.Data.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Data.Persistence.Context
{
    public partial class AppDbContext : ModuleDbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contractor> Contractors { get; set; }
        public virtual DbSet<ContractorAvatar> Avatars { get; set; }
        public virtual DbSet<ContractorLocalPermission> ContractorPermissions { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<ContractorDepartment> ContractorDepartments { get; set; }

        public virtual DbSet<Attestation> Attestations { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<Bonus> Bonuses { get; set; }
        public virtual DbSet<Penalization> Penalizations { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<FamilyMember> FamilyMembers { get; set; }
        public virtual DbSet<Vacation> Vacations { get; set; }
        public virtual DbSet<DismissalRequest> DismissalRequests { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<TimeSheetTable> TimeSheetTables { get; set; }
        public virtual DbSet<DocumentTemplate> DocumentTemplates { get; set; }

        public virtual DbSet<OrganizationRole> OrganizationRoles { get; set; }
        public virtual DbSet<OrganizationalChart> OrganizationalCharts { get; set; }
        public virtual DbSet<ContractorFile> ContractorFiles { get; set; }


        #region Organization-role relations

        public virtual DbSet<DepartmentRoleRelation> DepartmentRoleRelations { get; set; }
        public virtual DbSet<ParentDepartmentChildDepartment> ParentDepartmentChildDepartments { get; set; }
        public virtual DbSet<ParentDepartmentChildOrganizationRole> ParentDepartmentChildOrganizationRoles { get; set; }
        public virtual DbSet<ParentOrganizationRoleChildOrganizationRole> ParentOrganizationRoleChildOrganizationRoles { get; set; }
        public virtual DbSet<ParentOrganizationRoleChildDepartment> ParentOrganizationRoleChildDepartments { get; set; }

        #endregion

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
       
        public virtual DbSet<DepartmentRoleContent> DepartmentRoleContents { get; set; }


        #region config
        public virtual DbSet<VacationConfiguration> VacationConfigurations { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }

        #endregion

        #region NomenclatureTypes
        public virtual DbSet<NomenclatureType> NomenclatureTypes { get; set; }

        public virtual DbSet<NomenclatureColumn> NomenclatureColumns { get; set; }

        public virtual DbSet<NomenclatureRecord> NomenclatureRecords { get; set; }

        public virtual DbSet<RecordValue> RecordValues { get; set; }

        public virtual DbSet<RecordValueBoolean> RecordValueBooleans { get; set; }
        public virtual DbSet<RecordValueChar> RecordValueChars { get; set; }
        public virtual DbSet<RecordValueDate> RecordValueDates { get; set; }
        public virtual DbSet<RecordValueDateTime> RecordValuesDateTimes { get; set; }
        public virtual DbSet<RecordValueDouble> RecordValueDoubles { get; set; }
        public virtual DbSet<RecordValueEmail> RecordValueEmails { get; set; }
        public virtual DbSet<RecordValueInteger> RecordValueIntegers { get; set; }
        public virtual DbSet<RecordValueText> RecordValueTexts { get; set; }
        #endregion


        public virtual DbSet<Study> Studies { get; set; }
        public virtual DbSet<Bulletin> Bulletins { get; set; }

        public virtual DbSet<IndividualContract> Contracts { get; set; }
        public virtual DbSet<Instruction> Instructions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Department-Department
            modelBuilder.Entity<ParentDepartmentChildDepartment>()
                .Property(x => x.ParentDepartmentId)
                .HasColumnName("ParentDepartmentId");

            modelBuilder.Entity<ParentDepartmentChildDepartment>()
                .Property(x => x.ChildDepartmentId)
                .HasColumnName("ChildDepartmentId");
            #endregion

            #region Department-OrganizationRole
            modelBuilder.Entity<ParentDepartmentChildOrganizationRole>()
                .Property(x => x.ParentDepartmentId)
                .HasColumnName("ParentDepartmentId");

            modelBuilder.Entity<ParentDepartmentChildOrganizationRole>()
                .Property(x => x.ChildOrganizationRoleId)
                .HasColumnName("ChildOrganizationRoleId");
            #endregion

            #region OrganizationRole-Department
            modelBuilder.Entity<ParentOrganizationRoleChildDepartment>()
                .Property(x => x.ParentOrganizationRoleId)
                .HasColumnName("ParentOrganizationRoleId");

            modelBuilder.Entity<ParentOrganizationRoleChildDepartment>()
                .Property(x => x.ChildDepartmentId)
                .HasColumnName("ChildDepartmentId");
            #endregion

            #region OrganizationRole-OrganizationRole
            modelBuilder.Entity<ParentOrganizationRoleChildOrganizationRole>()
                .Property(x => x.ParentOrganizationRoleId)
                .HasColumnName("ParentOrganizationRoleId");

            modelBuilder.Entity<ParentOrganizationRoleChildOrganizationRole>()
                .Property(x => x.ChildOrganizationRoleId)
                .HasColumnName("ChildOrganizationRoleId");
            #endregion

            #region NomenclatureTypes

            modelBuilder.Entity<RecordValueBoolean>().Property(x => x.Value).HasColumnName("ValueAsBoolean");
            modelBuilder.Entity<RecordValueChar>().Property(x => x.Value).HasColumnName("ValueAsChar");
            modelBuilder.Entity<RecordValueDate>().Property(x => x.Value).HasColumnName("ValueAsDateTime");
            modelBuilder.Entity<RecordValueDateTime>().Property(x => x.Value).HasColumnName("ValueAsDateTime");
            modelBuilder.Entity<RecordValueDouble>().Property(x => x.Value).HasColumnName("ValueAsDouble");
            modelBuilder.Entity<RecordValueEmail>().Property(x => x.Value).HasColumnName("ValueAsText");
            modelBuilder.Entity<RecordValueInteger>().Property(x => x.Value).HasColumnName("ValueAsInteger");
            modelBuilder.Entity<RecordValueText>().Property(x => x.Value).HasColumnName("ValueAsText");

            #endregion

            //modelBuilder.Entity<Contractor>()
            //    .HasMany<IndividualContract>()
            //    .WithOne(x => x.Contractor)
            //    .HasForeignKey(x => x.ContractorId);

            //modelBuilder.Entity<Contractor>()
            //    .HasMany<IndividualContract>()
            //    .WithOne(x => x.Contractor)
            //    .HasForeignKey(x => x.SuperiorId);

            modelBuilder.Entity<IndividualContract>()
                .HasOne(d => d.Superior)
                .WithMany(x => x.Contractors)
                .HasForeignKey(x => x.SuperiorId);

            modelBuilder.Entity<IndividualContract>()
                .HasOne(d => d.Contractor)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.ContractorId);
        }
    }

}
