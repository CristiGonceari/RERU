using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTemplateQuestionCategories
{
    public class QuestionCategoryPreviewDto
    {
        public int TestTemplateId { get; set; }
        public int CategoryId { get; set; }
        public int? QuestionCount { get; set; }
        public QuestionTypeEnum? QuestionType { get; set; }
        public SelectionEnum SelectionType { get; set; }
        public SequenceEnum SequenceType { get; set; }
        public List<TestTemplateQuestionCategoryOrderDto> SelectedQuestions { get; set; }
    }
}
