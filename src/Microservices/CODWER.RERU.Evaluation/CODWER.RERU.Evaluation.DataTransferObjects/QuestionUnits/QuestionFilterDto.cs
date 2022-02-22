using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits
{
    public class QuestionFilterDto
    {
        public string QuestionName { get; set; }
        public string CategoryName { get; set; }
        public int? QuestionCategoryId { get; set; }
        public string QuestionTags { get; set; }
        public QuestionTypeEnum? Type { get; set; }
        public QuestionUnitStatusEnum? Status { get; set; }
    }
}
