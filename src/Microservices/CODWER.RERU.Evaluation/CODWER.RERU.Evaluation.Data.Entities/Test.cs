using System;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class Test : SoftDeleteBaseEntity
    {
        public Test()
        {
            TestQuestions = new HashSet<TestQuestion>();
        }

        public int UserProfileId { get; set; }
        public int? EvaluatorId { get; set; }
        public int TestTypeId { get; set; }
        public int? EventId { get; set; }
        public int? LocationId { get; set; }
        public TestPassStatusEnum? TestPassStatus { get; set; }
        public int? MaxErrors { get; set; }
        public TestStatusEnum TestStatus { get; set; }
        public TestResultStatusEnum ResultStatus { get; set; }
        public int? AccumulatedPercentage { get; set; }
        public bool? ShowUserName { get; set; }
        public DateTime ProgrammedTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TestTemplate TestTemplates { get; set; }
        public UserProfile UserProfile { get; set; }
        public UserProfile Evaluator { get; set; }
        public Event Event { get; set; }
        public Location Location { get; set; }

        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
    }
}
