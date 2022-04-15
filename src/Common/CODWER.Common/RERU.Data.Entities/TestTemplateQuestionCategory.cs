using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class TestTemplateQuestionCategory : SoftDeleteBaseEntity
    {
        public TestTemplateQuestionCategory()
        {
            TestCategoryQuestions = new HashSet<TestCategoryQuestion>();
        }

        public int CategoryIndex { get; set; }
        public int? QuestionCount { get; set; }
        public int? TimeLimit { get; set; }

        public QuestionTypeEnum? QuestionType { get; set; }
        public SelectionEnum SelectionType { get; set; }
        public SequenceEnum SequenceType { get; set; }

        public int TestTemplateId { get; set; }
        public TestTemplate TestTemplate { get; set; }

        public int QuestionCategoryId { get; set; }
        public QuestionCategory QuestionCategory { get; set; }

        public virtual ICollection<TestCategoryQuestion> TestCategoryQuestions { get; set; }
    }
}
