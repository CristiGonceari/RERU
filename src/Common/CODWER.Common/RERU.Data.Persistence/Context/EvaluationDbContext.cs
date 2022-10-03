using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Documents;

namespace RERU.Data.Persistence.Context
{
    public partial class AppDbContext
    {
        public virtual DbSet<QuestionCategory> QuestionCategories { get; set; }
        public virtual DbSet<TestTemplateQuestionCategory> TestTemplateQuestionCategories { get; set; }
        public virtual DbSet<TestTemplate> TestTemplates { get; set; }
        public virtual DbSet<TestTemplateSettings> TestTemplateSettings { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<QuestionUnit> QuestionUnits { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<TestQuestion> TestQuestions { get; set; }
        public virtual DbSet<TestAnswer> TestAnswers { get; set; }
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
        public virtual DbSet<FileTestAnswer> FileTestAnswers { get; set; }
        public virtual DbSet<SolicitedVacantPosition> SolicitedVacantPositions { get; set; }
        public virtual DbSet<RequiredDocument> RequiredDocuments { get; set; }
        public virtual DbSet<RequiredDocumentPosition> RequiredDocumentPositions { get; set; }
        public virtual DbSet<EventVacantPosition> EventVacantPositions { get; set; }
        public virtual DbSet<DocumentTemplate> DocumentTemplates { get; set; }
        public virtual DbSet<DocumentTemplateKey> DocumentTemplateKeys { get; set; }
        //public virtual DbSet<Department> Departments { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<EmailVerification> EmailVerifications { get; set; }
        public virtual DbSet<SolicitedVacantPositionUserFile> SolicitedVacantPositionUserFiles { get; set; }
        public virtual DbSet<SolicitedVacantPositionEmailMessage> SolicitedVacantPositionEmailMessages { get; set; }
        public virtual DbSet<EmailNotification> EmailNotifications { get; set; }
        public virtual DbSet<EmailNotificationProperty> EmailNotificationProperties { get; set; }
    }
}
