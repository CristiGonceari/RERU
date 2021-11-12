using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeByStatus
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPES_GENERAL_ACCESS)]
    public class GetTestTypeByStatusQuery : IRequest<List<SelectTestTypeValueDto>>
    {
        public TestTypeStatusEnum TestTypeStatus { get; set; }
        public int? EventId { get; set; }
    }
}
