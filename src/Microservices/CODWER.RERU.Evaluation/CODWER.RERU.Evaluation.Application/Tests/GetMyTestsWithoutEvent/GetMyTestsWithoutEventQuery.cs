using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyTestsWithoutEvent
{
    public class GetMyTestsWithoutEventQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
    }
}
