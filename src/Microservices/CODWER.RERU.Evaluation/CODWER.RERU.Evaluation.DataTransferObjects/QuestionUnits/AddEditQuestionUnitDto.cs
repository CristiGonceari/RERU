using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits
{
    public class AddEditQuestionUnitDto
    {
        public int? Id { get; set; }
        public int QuestionCategoryId { get; set; }
        public string Question { get; set; }
        public List<string> Tags { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public QuestionUnitStatusEnum Status { get; set; }
        public int QuestionPoints { get; set; }
    }
}
