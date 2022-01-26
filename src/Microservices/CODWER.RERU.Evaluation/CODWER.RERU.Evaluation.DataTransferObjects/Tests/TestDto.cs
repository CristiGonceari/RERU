using System;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Tests
{
    public class TestDto
    {
        public int Id { get; set; }        
        public int UserId { get; set; }        
        public int? EvaluatorId { get; set; }
        public int TestTypeId { get; set; }
        public string EventName { get; set; }
        public int EventId { get; set; }
        public string LocationName { get; set; }
        public TestPassStatusEnum? TestPassStatus { get; set; }
        public int? MaxErrors { get; set; }
        public int Duration { get; set; }        
        public int MinPercent { get; set; }
        public int QuestionCount { get; set; }
        public int AccumulatedPercentage { get; set; }
        public string UserName { get; set; }
        public string TestTypeName { get; set; }
        public string Rules { get; set; }
        public string VerificationProgress { get; set; }
        public bool ShowUserName { get; set; }
        public bool IsEvaluator { get; set; }
        public TestStatusEnum TestStatus { get; set; }
        public TestTypeModeEnum ModeStatus { get; set; }
        public TestResultStatusEnum Result { get; set; }
        public DateTime ProgrammedTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? ViewTestResult { get; set; }
    }
}