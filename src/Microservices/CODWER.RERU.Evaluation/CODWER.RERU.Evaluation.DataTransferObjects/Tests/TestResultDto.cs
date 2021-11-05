using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Tests
{
    public class TestResultDto
    {
        public int MinPercent { get; set; }
        public int AccumulatedPercentage { get; set; }
        public TestStatusEnum Status { get; set; }
        public TestResultStatusEnum Result { get; set; }
    }
}
