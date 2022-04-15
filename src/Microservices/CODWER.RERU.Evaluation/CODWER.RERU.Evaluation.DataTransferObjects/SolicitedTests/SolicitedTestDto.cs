using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests
{
    public class SolicitedTestDto
    {
        public int? Id { get; set; }
        public int? EventId { get; set; }
        public string EventName { get; set; }
        public int? UserProfileId { get; set; }
        public string UserProfileName { get; set; }
        public string UserProfileIdnp { get; set; }
        public int TestTemplateId { get; set; }
        public string TestTemplateName { get; set; }
        public DateTime SolicitedTime { get; set; }
        public SolicitedTestStatusEnum? SolicitedTestStatus { get; set; }
    }
}
