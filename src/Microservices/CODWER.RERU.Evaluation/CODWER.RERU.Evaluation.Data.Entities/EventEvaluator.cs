using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class EventEvaluator : SoftDeleteBaseEntity
    {
        public bool ShowUserName { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int EvaluatorId { get; set; }
        public UserProfile Evaluator { get; set; }
    }
}
