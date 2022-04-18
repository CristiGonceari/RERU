using CVU.ERP.Common.Data.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
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
        public virtual DbSet<SolicitedTest> SolicitedTests { get; set; }
        public virtual DbSet<DocumentTemplate> DocumentTemplates { get; set; }
        public virtual DbSet<DocumentTemplateKey> DocumentTemplateKeys { get; set; }

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
    }
}
