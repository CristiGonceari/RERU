using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.ServiceProvider.Clients;
using CVU.ERP.ServiceProvider.Models;
using MediatR;

namespace CODWER.RERU.Personal.Application.UserProfiles.GetAvailableModuleRoles
{
    public class GetAvailableModuleRolesQueryHandler : IRequestHandler<GetAvailableModuleRolesQuery, List<ModuleRolesDto>>
    {
        private readonly ICoreClient _coreClient;

        public GetAvailableModuleRolesQueryHandler(ICoreClient coreClient)
        {
            _coreClient = coreClient;
        }

        public Task<List<ModuleRolesDto>> Handle(GetAvailableModuleRolesQuery request, CancellationToken cancellationToken)
        {
            return _coreClient.GetModuleRoles();
        }
    }
}
