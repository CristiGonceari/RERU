using System.Collections.Generic;
using CVU.ERP.Module.Application.Models;
using MediatR;

namespace CODWER.RERU.Personal.Application.UserProfiles.GetAvailableModuleRoles
{
    public class GetAvailableModuleRolesQuery : IRequest<List<ModuleRolesDto>>
    {
    }
}
