using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes.GetNoAssignedTestTypes
{
    [ModuleOperation(permission: PermissionCodes.EVENT_TEST_TYPES_GENERAL_ACCESS)]

    public class GetNoAssignedTestTypesQuery : IRequest<List<TestTypeDto>>
    {
        public int EventId { get; set; }
        public string Keyword { get; set; }
    }
}
