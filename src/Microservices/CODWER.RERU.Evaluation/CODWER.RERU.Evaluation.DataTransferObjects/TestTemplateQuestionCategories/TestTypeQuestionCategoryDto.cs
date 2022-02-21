using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTemplateQuestionCategories
{
    public class TestTemplateQuestionCategoryDto
    {
        public int Id { get; set; }
        public int QuestionCategoryId { get; set; }
        public int CategoryIndex { get; set; }
        public string CategoryName { get; set; }
        public int? TimeLimit { get; set; }
        public QuestionTypeEnum? QuestionType { get; set; }
        public SelectionEnum SelectionType { get; set; }
        public SequenceEnum SequenceType { get; set; }
        public int? QuestionCount { get; set; }
        public QuestionUnit QuestionUnits { get; set; }

    }
}
