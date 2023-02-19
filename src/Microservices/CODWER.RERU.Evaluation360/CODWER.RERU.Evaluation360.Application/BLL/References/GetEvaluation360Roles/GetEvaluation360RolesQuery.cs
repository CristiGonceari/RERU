using System.Collections.Generic;
using CODWER.RERU.Evaluation360.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.References.GetEvaluation360Roles
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class GetEvaluation360RolesQuery : IRequest<List<SelectItem>>
    {
    }
}
