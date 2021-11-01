﻿using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class TestQuestion : SoftDeleteBaseEntity
    {
        public TestQuestion()
        {
            TestAnswers = new HashSet<TestAnswer>();
        }

        public int Index { get; set; }
        public int QuestionUnitId { get; set; }
        public int TestId { get; set; }
        public AnswerStatusEnum AnswerStatus { get; set; }
        public VerificationStatusEnum? Verified { get; set; }
        public int? TimeLimit { get; set; }
        public string Comment { get; set; }
        public bool? IsCorrect { get; set; }
        public int? Points { get; set; }
        public QuestionUnit QuestionUnit { get; set; }
        public Test Test { get; set; }

        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
    }
}
