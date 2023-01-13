using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations
{
    public class PrintTableEvaluationsDto
    {
        public int Id { set; get; }
        public string EvaluatedName { set; get; }
        public string EvaluatorName { set; get; }
        public string CounterSignerName { set; get; }
        public EvaluationTypeEnum? Type { set; get; }
        public decimal Points { set; get; }
        public EvaluationStatusEnum? Status { set; get; }
        public DateTime? CreateDate { set; get; }
        public bool canEvaluate { set; get; }
        public bool canDelete { set; get; }
        public bool canAccept { set; get; }
        public bool canCounterSign { set; get; }
        public bool canFinished { set; get; }
        public bool canDownload { set; get; }
        public int EvaluatorUserProfileId { get; set; }
        public int EvaluatedUserProfileId { get; set; }
        public int CounterSignerUserProfileId { get; set; }
    }
}