using System;
using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class Test : SoftDeleteBaseEntity
    {
        public Test()
        {
            TestQuestions = new HashSet<TestQuestion>();
            EmailTestNotifications = new HashSet<EmailTestNotification>();
        }

        public int? AccumulatedPercentage { get; set; }
        public bool? ShowUserName { get; set; }
        public int? MaxErrors { get; set; }

        public TestPassStatusEnum? TestPassStatus { get; set; }
        public TestStatusEnum TestStatus { get; set; }
        public TestResultStatusEnum ResultStatus { get; set; }
        public string HashGroupKey { get; set; }

        public string ResultStatusValue => ResultStatus == TestResultStatusEnum.Recommended
            ? $"{ResultStatus}:({RecommendedForValue}/{NotRecommendedForValue})" : ResultStatus.ToString();

        public string RecommendedFor { get; set; }
        public string RecommendedForValue => string.IsNullOrEmpty(RecommendedFor) ? "x" : RecommendedFor;
        public string NotRecommendedFor { get; set; }
        public string NotRecommendedForValue => string.IsNullOrEmpty(NotRecommendedFor) ? "x" : NotRecommendedFor;

        public DateTime ProgrammedTime { get; set; }
        public DateTime? EndProgrammedTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? SolicitedTime { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int? EvaluatorId { get; set; }
        public UserProfile Evaluator { get; set; }

        public int TestTemplateId { get; set; }
        public TestTemplate TestTemplate { get; set; }

        public int? EventId { get; set; }
        public Event Event { get; set; }

        public int? LocationId { get; set; }
        public Location Location { get; set; }
      

        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
        public virtual ICollection<EmailTestNotification> EmailTestNotifications { get; set; }
    }
}
