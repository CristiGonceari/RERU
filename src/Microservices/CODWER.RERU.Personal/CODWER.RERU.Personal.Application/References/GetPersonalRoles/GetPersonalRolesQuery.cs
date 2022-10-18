using System.Collections.Generic;
using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.References.GetPersonalRoles
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ARTICOLE)]
    public class GetPersonalRolesQuery : IRequest<List<SelectItem>>
    {
    }
}
