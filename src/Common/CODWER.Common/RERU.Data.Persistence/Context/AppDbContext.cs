using CVU.ERP.Common.Data.Persistence.EntityFramework;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RERU.Data.Entities;
using RERU.Data.Entities.Documents;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords.RecordValues;
using RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations;
using SpatialFocus.EntityFrameworkCore.Extensions;

namespace RERU.Data.Persistence.Context
{
    public partial class AppDbContext : ModuleDbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<RegistrationFluxStep> RegistrationFluxSteps { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test>()
                .HasOne(t => t.Evaluator)
                .WithMany(e => e.Tests)
                .HasForeignKey(t => t.EvaluatorId);

            modelBuilder.Entity<Module>().HasKey(c => c.Id);
            modelBuilder.Entity<UserProfile>().HasKey(c => c.Id);

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.Department)
                .WithMany()
                .HasForeignKey(up => up.DepartmentColaboratorId)
                .HasPrincipalKey(d => d.ColaboratorId);

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.Role)
                .WithMany()
                .HasForeignKey(up => up.RoleColaboratorId)
                .HasPrincipalKey(r => r.ColaboratorId);

            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(AppDbContext)
                    .Assembly);

            modelBuilder
                .ConfigureEnumLookup(EnumLookupOptions
                    .Default
                    .SetNamingScheme(n => n)
                    .UseNumberAsIdentifier()
                    .SetDeleteBehavior(DeleteBehavior.Restrict));












            #region HR

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


            modelBuilder.Entity<IndividualContract>()
                .HasOne(d => d.Superior)
                .WithMany(x => x.Contractors)
                .HasForeignKey(x => x.SuperiorId);

            modelBuilder.Entity<IndividualContract>()
                .HasOne(d => d.Contractor)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.ContractorId);



            #region Department-Department
            modelBuilder.Entity<ParentDepartmentChildDepartment>()
                .Property(x => x.ParentDepartmentId)
                .HasColumnName("ParentDepartmentId");

            modelBuilder.Entity<ParentDepartmentChildDepartment>()
                .Property(x => x.ChildDepartmentId)
                .HasColumnName("ChildDepartmentId");
            #endregion

            #region Department-Role
            modelBuilder.Entity<ParentDepartmentChildRole>()
                .Property(x => x.ParentDepartmentId)
                .HasColumnName("ParentDepartmentId");

            modelBuilder.Entity<ParentDepartmentChildRole>()
                .Property(x => x.ChildRoleId)
                .HasColumnName("ChildRoleId");
            #endregion

            #region Role-Department
            modelBuilder.Entity<ParentRoleChildDepartment>()
                .Property(x => x.ParentRoleId)
                .HasColumnName("ParentRoleId");

            modelBuilder.Entity<ParentRoleChildDepartment>()
                .Property(x => x.ChildDepartmentId)
                .HasColumnName("ChildDepartmentId");
            #endregion

            #region Role-Role
            modelBuilder.Entity<ParentRoleChildRole>()
                .Property(x => x.ParentRoleId)
                .HasColumnName("ParentRoleId");

            modelBuilder.Entity<ParentRoleChildRole>()
                .Property(x => x.ChildRoleId)
                .HasColumnName("ChildRoleId");
            #endregion

            #endregion
        }

        ///<summary>
        ///Get new instance of AppDbContext for thread safe using
        ///</summary>
        public static AppDbContext NewInstance(IConfiguration configuration) => new(new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(configuration.GetConnectionString(ConnectionString.Common))
            .Options);
    }
}
