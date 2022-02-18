using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class EventTestType : SoftDeleteBaseEntity
    {
        public int MaxAttempts { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int TestTemplateId { get; set; }
        public TestTemplate TestTemplate { get; set; }
    }
}
