using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;
using System;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyTestsCountWithoutEvent
{
    public class GetMyTestsCountWithoutEventQuery : IRequest<List<TestCount>>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
