using CVU.ERP.Common.Data.Entities;
using System;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class SolicitedTest : SoftDeleteBaseEntity
    {
        public int? EventId { get; set; }
        public Event Event { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int TestTemplateId { get; set; }
        public TestTemplate TestTemplate { get; set; }

        public int? CandidatePositionId { get; set; }
        public CandidatePosition CandidatePosition { get; set; }

        public DateTime SolicitedTime { get; set; }
        public SolicitedTestStatusEnum SolicitedTestStatus { get; set; }
    }
}
