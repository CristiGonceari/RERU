using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits
{
    public class EditQuestionStatusDto
    {
        public int QuestionId { get; set; }
        public QuestionUnitStatusEnum Status { get; set; }
    }
}
