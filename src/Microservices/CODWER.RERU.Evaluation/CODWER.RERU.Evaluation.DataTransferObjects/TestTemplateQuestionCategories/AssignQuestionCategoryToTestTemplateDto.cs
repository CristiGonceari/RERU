using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTemplateQuestionCategories
{
    public class AssignQuestionCategoryToTestTemplateDto
    {
        public int TestTemplateId { get; set; }
        public int QuestionCategoryId { get; set; }
        public int? CategoryIndex { get; set; }
        public int? QuestionCount { get; set; }
        public int? TimeLimit { get; set; }
        public QuestionTypeEnum? QuestionType { get; set; }
        public SelectionEnum SelectionType { get; set; }
        public SequenceEnum SequenceType { get; set; }
        public List<TestCategoryQuestionDto> TestCategoryQuestions { get; set; }
    }
}