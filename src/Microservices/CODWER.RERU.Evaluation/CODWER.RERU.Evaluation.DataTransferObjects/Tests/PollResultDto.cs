using System;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Tests
{
    public class PollResultDto
    {
        public int Id { get; set; }
        public string TestTemplateName { get; set; }
        public string EventName { get; set; }
        public int? TotalInvited { get; set; }
        public int TotalVotedCount { get; set; }
        public double TotalVotedPercent { get; set; }
        public int ItemsCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PollQuestionDto> Questions { get; set; }
    }
}
