using System;

namespace CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations
{
    public class CounterSignAcceptRejectEvaluationDto
    {
        public int Id { set; get; }
        public bool? CheckComment1 { set; get; }
        public bool? CheckComment2 { set; get; }
        public bool? CheckComment3 { set; get; }
        public bool? CheckComment4 { set; get; }
        public string? OtherComments { set; get; }
        public string? FunctionCounterSigner { set; get; }
        public DateTime? DateCompletionCounterSigner { set; get; }
        public bool? SignatureCounterSigner { set; get; }
    }
}