using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Module.Common.Models;
using CVU.ERP.Module.Common.Providers;
using Microsoft.AspNetCore.Mvc;

namespace CVU.ERP.Module.API.Controllers.Internal {
    [ApiController]
    [Route ("internal/api/[controller]")]
    public class PermissionController : Controller {
        private readonly IEnumerable<IModulePermissionProvider> _modulePermissionProviders;
        public PermissionController (IEnumerable<IModulePermissionProvider> modulePermissionProviders) {
            _modulePermissionProviders = modulePermissionProviders;
        }

        [HttpGet]
        public async Task<ModulePermission[]> GetPermissions () {
            var permissions = new List<ModulePermission> ();

            foreach (var provider in _modulePermissionProviders) {
                permissions.AddRange (await provider.Get ());
            }
            return permissions.ToArray ();
        }
    }
}