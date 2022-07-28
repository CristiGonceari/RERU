//using System.Threading.Tasks;
//using CVU.ERP.Module.Application.Clients;
//using CVU.ERP.Module.Application.Models;

//namespace CVU.ERP.Module.Application.Providers
//{
//    public class ModuleApplicationUserProvider : IApplicationUserProvider
//    {
//        private readonly ICoreClient _coreClient;
//        public ModuleApplicationUserProvider(ICoreClient coreClient)
//        {
//            _coreClient = coreClient;
//        }

//        public async Task<ApplicationUser> Get(string id, string identityProvider = null)
//        {
//            return await _coreClient.GetApplicationUserByIdentity(id, identityProvider);
//        }
//    }
//}