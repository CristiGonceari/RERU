using CODWER.RERU.Evaluation.Data.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests
{
    public class AddTestsCommand : IRequest<List<int>>
    {
        public int Id { get; set; }
        public List<int> UserProfileId { get; set; }
        public int? EvaluatorId { get; set; }
        public bool? ShowUserName { get; set; }
        public int TestTemplateId { get; set; }
        public int? EventId { get; set; }
        public int? LocationId { get; set; }
        public TestStatusEnum TestStatus { get; set; }
        public DateTime ProgrammedTime { get; set; }
    }
}
