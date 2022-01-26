using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Providers;
using CVU.ERP.Module.Common.Providers;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class MockCurrentApplicationUserProvider : ICurrentApplicationUserProvider
    {
        private readonly IModulePermissionProvider _modulePermissionProvider;
        public MockCurrentApplicationUserProvider(IModulePermissionProvider modulePermissionProvider)
        {
            _modulePermissionProvider = modulePermissionProvider;
        }

        public bool IsAuthenticated => true;

        public string IdentityId => "827d8e7c-4526-4965-b376-50ff9bf2cf5d";

        public string IdentityProvider => throw new System.NotImplementedException();

        public async Task<ApplicationUser> Get()
        {
            return new()
            {
                Id = "827d8e7c-4526-4965-b376-50ff9bf2cf5d",
                Name = "Andrian Hubencu",
                Email = "hubencu.andrian@gmail.com",
                Modules = new List<ApplicationUserModule>()
                {
                    new()
                    {
                        Permissions = (await _modulePermissionProvider.Get()).Select(x => x.Code).ToList()
                    }
                }
            };
        }
    }
}