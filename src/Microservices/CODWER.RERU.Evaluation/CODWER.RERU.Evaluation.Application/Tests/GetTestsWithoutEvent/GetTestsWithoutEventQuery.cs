using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTestsWithoutEvent
{
    [ModuleOperation(permission: PermissionCodes.TESTS_GENERAL_ACCESS)]
    public class GetTestsWithoutEventQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
    }
}
