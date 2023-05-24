using System;
using System.Collections.Generic;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Tests.AddEvaluations
{
    public class AddEvaluationsCommand : IRequest<List<int>>
    {
        public int Id { get; set; }
        public List<int> UserProfileIds { get; set; }
        public List<int> EvaluatorIds { get; set; }
        public int TestTemplateId { get; set; }
        public int? EventId { get; set; }
        public int? LocationId { get; set; }
        public TestStatusEnum TestStatus { get; set; }
        public DateTime? ProgrammedTime { get; set; }
        public DateTime? SolicitedTime { get; set; }
        public int ProcessId { get; set; }
    }
}
