using System.Collections.Generic;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.ServiceProvider.Models;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.GetInternalModules
{
    public class GetInternalModulesQuery : IRequest<List<ModuleRolesDto>>
    {
    }
}
