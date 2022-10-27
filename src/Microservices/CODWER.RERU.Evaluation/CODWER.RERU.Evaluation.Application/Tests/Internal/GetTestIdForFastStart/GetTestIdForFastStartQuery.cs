using CODWER.RERU.Evaluation.DataTransferObjects.InternalTest;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Tests.Internal.GetTestIdForFastStart
{
    public class GetTestIdForFastStartQuery : IRequest<List<GetTestForFastStartDto>>
    {
    }
}
