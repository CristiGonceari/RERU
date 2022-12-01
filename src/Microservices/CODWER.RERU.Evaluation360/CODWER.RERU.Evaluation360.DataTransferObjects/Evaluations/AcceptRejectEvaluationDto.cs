using System;

namespace CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations
{
    public class AcceptRejectEvaluationDto
    {
        public int Id { set; get; }
        public string? CommentsEvaluated { set; get; }
        public DateTime? DateAcceptOrRejectEvaluated { set; get; }
        public bool? SignatureEvaluated { set; get; }
    }
}