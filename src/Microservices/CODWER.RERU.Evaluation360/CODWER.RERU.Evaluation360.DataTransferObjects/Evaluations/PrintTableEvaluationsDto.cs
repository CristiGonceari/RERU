using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;

    public class PrintTableEvaluationsDto
    {
        public string EvaluatedName { set; get; }
        public string EvaluatorName { set; get; }
        public string CounterSignerName { set; get; }
        public EvaluationTypeEnum? Type { set; get; }
        public decimal Points { set; get; }
        public EvaluationStatusEnum? Status { set; get; }
    }