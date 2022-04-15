using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using System.Collections.Generic;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions
{
    public class TestCategoryQuestionContentDto
    {
        public SequenceEnum SequenceType { get; set; }
        public int? UsedQuestionCount { get; set; }
        public string QuestionCategoryName { get; set; }
        public List<QuestionUnitDto> Questions { get; set; }
    }
}
