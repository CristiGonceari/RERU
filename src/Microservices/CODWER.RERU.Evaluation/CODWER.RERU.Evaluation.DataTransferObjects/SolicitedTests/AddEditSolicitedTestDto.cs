using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests
{
    public class AddEditSolicitedTestDto
    {
        public int? Id { get; set; }
        public int? EventId { get; set; }
        public int? UserProfileId { get; set; }
        public int TestTemplateId { get; set; }
        public DateTime SolicitedTime { get; set; }
        public SolicitedTestStatusEnum? SolicitedTestStatus { get; set; }
    }
}
