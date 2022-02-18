using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.Data.Entities;
using System;
using System.Collections.Generic;

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

        public string Question { get; set; }
        public int? QuestionPoints { get; set; }
        public string? MediaFileId { get; set; }

        public QuestionTypeEnum QuestionType { get; set; }
        public QuestionUnitStatusEnum Status { get; set; }

        public int QuestionCategoryId { get; set; }
        public QuestionCategory QuestionCategory { get; set; }

        public Guid PdfFileId { get; set; }

        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
        public virtual ICollection<QuestionUnitTag> QuestionUnitTags { get; set; }
    }
}
