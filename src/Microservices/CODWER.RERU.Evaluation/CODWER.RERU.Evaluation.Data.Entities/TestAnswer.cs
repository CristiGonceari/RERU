using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class TestAnswer : SoftDeleteBaseEntity
    {
        public int TestQuestionId { get; set; }
        public int? OptionId { get; set; }
        public string AnswerValue { get; set; }
        public TestQuestion TestQuestion { get; set; }
        public Option Option { get; set; }
    }
}
