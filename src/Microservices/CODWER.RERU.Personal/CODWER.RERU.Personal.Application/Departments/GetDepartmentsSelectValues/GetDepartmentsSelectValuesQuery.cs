using System.Collections.Generic;
using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Departments.GetDepartmentsSelectValues
{
    [ModuleOperation(permission: PermissionCodes.DEPARTMENTS_GENERAL_ACCESS)]

    public class GetDepartmentsSelectValuesQuery : IRequest<List<SelectItem>>
    {
    }
}
