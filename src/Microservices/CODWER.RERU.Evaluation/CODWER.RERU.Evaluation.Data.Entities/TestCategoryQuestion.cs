using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class TestCategoryQuestion : SoftDeleteBaseEntity
    {
        public int Index { get; set; }

        public int TestTypeQuestionCategoryId { get; set; }
        public TestTypeQuestionCategory TestTypeQuestionCategory { get; set; }

        public int QuestionUnitId { get; set; }
        public QuestionUnit QuestionUnit { get; set; }
    }
}
