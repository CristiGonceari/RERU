using System.Threading.Tasks;
using CVU.ERP.ServiceProvider.Clients;
using CVU.ERP.ServiceProvider.Models;

namespace CVU.ERP.ServiceProvider
{
    public class ModuleApplicationUserProvider : IApplicationUserProvider
    {
        private readonly ICoreClient _coreClient;
        public ModuleApplicationUserProvider(ICoreClient coreClient)
        {
            _coreClient = coreClient;
        }

        public async Task<ApplicationUser> Get(string id, string identityProvider = null)
        {
            return await _coreClient.GetApplicationUserByIdentity(id, identityProvider);
        }
    }
}