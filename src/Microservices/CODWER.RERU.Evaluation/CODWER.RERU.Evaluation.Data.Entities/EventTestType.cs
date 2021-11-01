using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class EventTestType : SoftDeleteBaseEntity
    {
        public int EventId { get; set; }
        public int TestTypeId { get; set; }
        public int MaxAttempts { get; set; }
        public Event Event { get; set; }
        public TestType TestType { get; set; }
    }
}
