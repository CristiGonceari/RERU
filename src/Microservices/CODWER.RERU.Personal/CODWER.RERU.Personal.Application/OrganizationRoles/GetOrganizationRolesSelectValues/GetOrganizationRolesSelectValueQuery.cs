using System.Collections.Generic;
using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRolesSelectValues
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ROLURI)]

    public class GetRolesSelectValuesQuery : IRequest<List<SelectItem>>
    {
        public string SearchWord { get; set; }
    }
}
