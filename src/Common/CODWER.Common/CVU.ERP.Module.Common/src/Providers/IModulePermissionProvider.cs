using System.Threading.Tasks;
using CVU.ERP.Module.Common.Models;

namespace CVU.ERP.Module.Common.Providers {
    public interface IModulePermissionProvider {
        Task<ModulePermission[]> Get ();
    }
}