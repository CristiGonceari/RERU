using CVU.ERP.Common.Data.Persistence.EntityFramework;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RERU.Data.Entities;
using RERU.Data.Entities.Documents;
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

        public DbSet<Module> Modules { set; get; }
        public DbSet<ModulePermission> ModulePermissions { set; get; }
        public DbSet<ModuleRole> ModuleRoles { set; get; }
        public DbSet<ModuleRolePermission> ModuleRolePermissions { set; get; }
        public DbSet<UserProfileModuleRole> UserProfileModuleRoles { set; get; }
        public DbSet<CandidatePosition> CandidatePositions { set; get; }
        public DbSet<UserFile> UserFiles { set; get; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<QuestionCategory> QuestionCategories { get; set; }
        public virtual DbSet<TestTemplateQuestionCategory> TestTemplateQuestionCategories { get; set; }
        public virtual DbSet<TestTemplate> TestTemplates { get; set; }
        public virtual DbSet<TestTemplateSettings> TestTemplateSettings { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<QuestionUnit> QuestionUnits { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<TestQuestion> TestQuestions { get; set; }
        public virtual DbSet<TestAnswer> TestAnswers { get; set; }
        public virtual DbSet<ArticleCore> CoreArticles { get; set; }
        public virtual DbSet<ArticleEvaluation> EvaluationArticles { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationResponsiblePerson> LocationResponsiblePersons { get; set; }
        public virtual DbSet<LocationClient> LocationClients { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventResponsiblePerson> EventResponsiblePersons { get; set; }
        public virtual DbSet<EventUser> EventUsers { get; set; }
        public virtual DbSet<EventLocation> EventLocations { get; set; }
        public virtual DbSet<EventEvaluator> EventEvaluators { get; set; }
        public virtual DbSet<EventTestTemplate> EventTestTemplates { get; set; }
        public virtual DbSet<TestCategoryQuestion> TestCategoryQuestions { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<PlanResponsiblePerson> PlanResponsiblePersons { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<QuestionUnitTag> QuestionUnitTags { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<EmailTestNotification> EmailTestNotifications { get; set; }
        public virtual DbSet<SolicitedVacantPosition> SolicitedVacantPositions { get; set; }
        public virtual DbSet<RequiredDocument> RequiredDocuments { get; set; }
        public virtual DbSet<RequiredDocumentPosition> RequiredDocumentPositions { get; set; }
        public virtual DbSet<EventVacantPosition> EventVacantPositions { get; set; }
        public virtual DbSet<DocumentTemplate> DocumentTemplates { get; set; }
        public virtual DbSet<DocumentTemplateKey> DocumentTemplateKeys { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RegistrationPageMessage> RegistrationPageMessages { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<EmailVerification> EmailVerifications { get; set; }

        #region FisaPersonala
        public virtual DbSet<CandidateNationality> CandidateNationalities { get; set; }
        public virtual DbSet<CandidateCitizenship> CandidateCitizens { get; set; }
        public virtual DbSet<Bulletin> Bulletins { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Study> Studies { get; set; }
        public virtual DbSet<StudyType> StudyTypes { get; set; }
        public virtual DbSet<ModernLanguageLevel> ModernLanguageLevels { get; set; }
        public virtual DbSet<ModernLanguage> ModernLanguages { get; set; }
        public virtual DbSet<RecommendationForStudy> RecommendationForStudies { get; set; }
        public virtual DbSet<MaterialStatus> MaterialStatuses { get; set; }
        public virtual DbSet<MaterialStatusType> MaterialStatusTypes { get; set; }
        public virtual DbSet<KinshipRelationWithUserProfile> KinshipRelationWithUserProfiles { get; set; }
        public virtual DbSet<KinshipRelation> KinshipRelations { get; set; }
        public virtual DbSet<KinshipRelationCriminalData> KinshipRelationCriminalDatas { get; set; }
        public virtual DbSet<MilitaryObligation> MilitaryObligations { get; set; }
        public virtual DbSet<Autobiography> Autobiographies { get; set; }
        #endregion


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
        }

        public static AppDbContext NewInstance(IConfiguration configuration) => new(new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(configuration.GetConnectionString(ConnectionString.Common))
            .Options);
    }
}
