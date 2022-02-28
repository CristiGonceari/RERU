using CODWER.RERU.Evaluation.Data.Entities;
using CVU.ERP.Common.Data.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Data.Persistence.Context
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
        public virtual DbSet<QuestionCategory> QuestionCategories { get; set; }
        public virtual DbSet<TestTemplateQuestionCategory> TestTemplateQuestionCategories { get; set; }
        public virtual DbSet<TestTemplate> TestTemplates { get; set; }
        public virtual DbSet<TestTemplateSettings> TestTemplateSettings { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<QuestionUnit> QuestionUnits { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<TestQuestion> TestQuestions { get; set; }
        public virtual DbSet<TestAnswer> TestAnswers { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
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
        }
    }
}
