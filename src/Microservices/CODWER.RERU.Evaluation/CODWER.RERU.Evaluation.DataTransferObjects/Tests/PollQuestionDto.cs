using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Tests
{
    public class PollQuestionDto
    {
        public int QuestionId { get; set; }
        public int Index { get; set; }
        public string Question { get; set; }
        public int? VotedCount { get; set; }
        public double VotedPercent { get; set; }
        public List<PollOptionDto> Options { get; set; }
    }
}
