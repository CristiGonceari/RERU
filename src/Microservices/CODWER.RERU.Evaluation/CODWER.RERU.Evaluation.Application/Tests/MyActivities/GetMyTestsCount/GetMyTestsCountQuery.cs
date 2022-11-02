using System;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyTestsCount
{
    public class GetMyTestsCountQuery : IRequest<List<TestCount>>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
