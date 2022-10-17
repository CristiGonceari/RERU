using System.Collections.Generic;
using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.References.GetCoreRoles
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class GetCoreRolesQuery : IRequest<List<SelectItem>>
    {
    }
}
