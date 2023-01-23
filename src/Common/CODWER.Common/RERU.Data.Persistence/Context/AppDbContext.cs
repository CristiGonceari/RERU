using System.Linq;
using CVU.ERP.Common.Data.Persistence.EntityFramework;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords.RecordValues;
using RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations;
using SpatialFocus.EntityFrameworkCore.Extensions;

namespace RERU.Data.Persistence.Context
{
    public partial class AppDbContext : ModuleDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private const string DEFAULT_IDENTITY_SERVICE = "local";

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) 
            : base(options)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _currentUserId = GetAuthenticatedUserId();
        }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<EmployeeFunction> EmployeeFunctions { get; set; }

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

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.EmployeeFunction)
                .WithMany()
                .HasForeignKey(up => up.FunctionColaboratorId)
                .HasPrincipalKey(r => r.ColaboratorId);

            modelBuilder.Entity<Contractor>()
                        .Ignore(b => b.FirstName)
                        .Ignore(b => b.LastName)
                        .Ignore(b => b.FatherName)
                        .Ignore(b => b.Idnp)
                        .Ignore(b => b.PhoneNumber)
                        .Ignore(b => b.BirthDate);

            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(AppDbContext)
                    .Assembly);

            modelBuilder
                .ConfigureEnumLookup(EnumLookupOptions
                    .Default
                    .SetNamingScheme(n => n)
                    .UseNumberAsIdentifier()
                    .SetDeleteBehavior(DeleteBehavior.Restrict));

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.Contractor)
                .WithOne(up => up.UserProfile)
                .HasForeignKey<Contractor>(b => b.UserProfileId);




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
        //public static AppDbContext NewInstance(IConfiguration configuration, ICurrentApplicationUserProvider currentApplicationUserProvider) 
        //    => new(new DbContextOptionsBuilder<AppDbContext>()
        //    .UseNpgsql(configuration.GetConnectionString(ConnectionString.Common))
        //    .Options, currentApplicationUserProvider, configuration);

        public AppDbContext NewInstance()
        {
            return new(new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(_configuration.GetConnectionString(ConnectionString.Common))
                .Options, _configuration, _httpContextAccessor);
        }


        #region Get Current User()

        public string GetAuthenticatedUserId() => IsAuthenticated() ? GetUserProfileId(IdentityId, IdentityProvider) : "0";

        public bool IsAuthenticated()
        {
            if (_httpContextAccessor == null) return false;
            if (_httpContextAccessor.HttpContext != null)
            {
                return _httpContextAccessor.HttpContext.User.Identity != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }

            return false;

        }
        //public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string IdentityId
        {
            get
            {
                var identityUser = _httpContextAccessor.HttpContext.User;
                return identityUser.FindFirst("sub")?.Value;
            }
        }

        public string IdentityProvider
        {
            get
            {
                var identityUser = _httpContextAccessor.HttpContext.User;
                return identityUser.FindFirst("idp")?.Value;
            }
        }

        private string GetUserProfileId(string id, string identityProvider = null)
        {
            identityProvider = identityProvider ?? DEFAULT_IDENTITY_SERVICE;

            var userProfile = UserProfiles
                .Include(x=>x.Identities)
                .FirstOrDefault(up => up.Identities.Any(upi => upi.Identificator == id && upi.Type == identityProvider));

            return userProfile != null ? userProfile.Id.ToString() : "0";
        }
        #endregion
    }
}
