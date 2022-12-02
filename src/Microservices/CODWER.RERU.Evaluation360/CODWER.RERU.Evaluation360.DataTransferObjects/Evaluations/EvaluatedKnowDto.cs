using System;

namespace CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations
{
    public class EvaluatedKnowDto
    {
        public int Id { set; get; }
        public string? NameSurnameEvaluated { set; get; }
        public DateTime? DateEvaluatedKnow { set; get; }
        public bool? SignatureEvaluated { set; get; }
    }
}