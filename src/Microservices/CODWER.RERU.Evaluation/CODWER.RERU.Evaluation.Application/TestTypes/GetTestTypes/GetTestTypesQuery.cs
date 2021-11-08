using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypes
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPES_GENERAL_ACCESS)]

    public class GetTestTypesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestTypeDto>>
    {
        public string Name { get; set; }
        public string EventName { get; set; }
        public TestTypeStatusEnum? Status { get; set; }
    }
}
