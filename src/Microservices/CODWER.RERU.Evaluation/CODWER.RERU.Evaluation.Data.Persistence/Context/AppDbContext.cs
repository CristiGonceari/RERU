using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Files;
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
        public virtual DbSet<TestTypeQuestionCategory> TestTypeQuestionCategories { get; set; }
        public virtual DbSet<TestType> TestTypes { get; set; }
        public virtual DbSet<TestTypeSettings> TestTypeSettings { get; set; }
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
        public virtual DbSet<EventTestType> EventTestTypes { get; set; }
        public virtual DbSet<TestCategoryQuestion> TestCategoryQuestions { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<PlanResponsiblePerson> PlanResponsiblePersons { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<QuestionUnitTag> QuestionUnitTags { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<File> Files { get; set; }

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
