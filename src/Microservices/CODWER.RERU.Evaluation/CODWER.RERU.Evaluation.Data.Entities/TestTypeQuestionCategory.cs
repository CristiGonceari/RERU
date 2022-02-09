using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class TestTypeQuestionCategory : SoftDeleteBaseEntity
    {
        public TestTypeQuestionCategory()
        {
            TestCategoryQuestions = new HashSet<TestCategoryQuestion>();
        }

        public int TestTypeId { get; set; }
        public int QuestionCategoryId { get; set; }
        public int CategoryIndex { get; set; }
        public QuestionTypeEnum? QuestionType { get; set; }
        public int? QuestionCount { get; set; }
        public int? TimeLimit { get; set; }
        public SelectionEnum SelectionType { get; set; }
        public SequenceEnum SequenceType { get; set; }
        public TestTemplate TestTemplates { get; set; }
        public QuestionCategory QuestionCategory { get; set; }

        public virtual ICollection<TestCategoryQuestion> TestCategoryQuestions { get; set; }
    }
}
