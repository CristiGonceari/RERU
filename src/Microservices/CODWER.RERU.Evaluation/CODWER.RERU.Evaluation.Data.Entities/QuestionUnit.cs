using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class QuestionUnit : SoftDeleteBaseEntity
    {
        public QuestionUnit()
        {
            Options = new HashSet<Option>();
            TestQuestions = new HashSet<TestQuestion>();
            QuestionUnitTags = new HashSet<QuestionUnitTag>();
        }

        public int QuestionCategoryId { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public QuestionUnitStatusEnum Status { get; set; }
        public string Question { get; set; }
        public int? QuestionPoints { get; set; }
        public QuestionCategory QuestionCategory { get; set; }

        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
        public virtual ICollection<QuestionUnitTag> QuestionUnitTags { get; set; }
    }
}
