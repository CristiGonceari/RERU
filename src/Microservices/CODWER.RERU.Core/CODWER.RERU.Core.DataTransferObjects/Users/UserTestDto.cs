using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class UserTestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TestTemplateId { get; set; }
        public string TestTemplateName { get; set; }
        public string EventName { get; set; }
        public int EventId { get; set; }
        public int MinPercent { get; set; }
        public int QuestionCount { get; set; }
        public int AccumulatedPercentage { get; set; }
        public string VerificationProgress { get; set; }
        public TestStatusEnum TestStatus { get; set; }
        public TestResultStatusEnum Result { get; set; }
        public string ResultStatusValue { get; set; }

    }
}
