using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes.GetEventTestTypes
{
    [ModuleOperation(permission: PermissionCodes.EVENT_TEST_TYPES_GENERAL_ACCESS)]
    public class GetEventTestTypesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestTypeDto>>
    {
        public int EventId { get; set; }
    }
}
