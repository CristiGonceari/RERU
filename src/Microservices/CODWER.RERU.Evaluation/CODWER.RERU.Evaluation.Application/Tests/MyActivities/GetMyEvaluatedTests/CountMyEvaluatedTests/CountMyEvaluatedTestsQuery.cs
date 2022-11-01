using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;
using System;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyEvaluatedTests.CountMyEvaluatedTests
{
    public class CountMyEvaluatedTestsQuery : IRequest<List<TestCount>>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
