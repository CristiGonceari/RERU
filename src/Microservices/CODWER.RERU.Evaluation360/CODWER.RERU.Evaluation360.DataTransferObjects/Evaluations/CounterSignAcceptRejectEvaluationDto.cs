using System;

namespace CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations
{
    public class CounterSignAcceptRejectEvaluationDto
    {
        public int Id { set; get; }
        public string CheckComment1 { set; get; }
        public string CheckComment2 { set; get; }
        public string CheckComment3 { set; get; }
        public string CheckComment4 { set; get; }
        public string OtherComments { set; get; }
        public string DecisionCountersignatory { set; get; }
        public string DateCompletionCountersignatory { set; get; }
    }
}