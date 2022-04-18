using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class TestAnswer : SoftDeleteBaseEntity
    {
        public string AnswerValue { get; set; }

        public int TestQuestionId { get; set; }
        public TestQuestion TestQuestion { get; set; }

        public int? OptionId { get; set; }
        public Option Option { get; set; }
    }
}
