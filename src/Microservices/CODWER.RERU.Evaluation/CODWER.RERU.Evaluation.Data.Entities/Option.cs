using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class Option : SoftDeleteBaseEntity
    {
        public int? InternalId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        public string? MediaFileId { get; set; }

        public int QuestionUnitId { get; set; }
        public virtual QuestionUnit QuestionUnit { get; set; }
    }
}
