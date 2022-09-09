using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class TestQuestion : SoftDeleteBaseEntity
    {
        public TestQuestion()
        {
            TestAnswers = new HashSet<TestAnswer>();
            FileTestAnswers = new HashSet<FileTestAnswer>();
        }

        public int Index { get; set; }
        public int? TimeLimit { get; set; }
        public string Comment { get; set; }
        public bool? IsCorrect { get; set; }
        public int? Points { get; set; }

        public AnswerStatusEnum AnswerStatus { get; set; }
        public VerificationStatusEnum? Verified { get; set; }

        public int QuestionUnitId { get; set; }
        public QuestionUnit QuestionUnit { get; set; }
        
        public int TestId { get; set; }
        public Test Test { get; set; }

        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
        public virtual ICollection<FileTestAnswer> FileTestAnswers { get; set; }
    }
}
