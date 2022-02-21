using System;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Tests
{
    public class PollDto
    {
        public int Id { get; set; }
        public string TestTemplateName { get; set; }
        public MyPollStatusEnum Status { get; set; }
        public TestStatusEnum? TestStatus { get; set; }
        public TestTemplateStatusEnum TestTemplateStatus { get; set; }
        public bool Setting { get; set; }
        public DateTime? VotedTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
