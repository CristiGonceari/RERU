using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Module.Common.Models;
using CVU.ERP.ServiceProvider.Models;

namespace CVU.ERP.ServiceProvider.Clients {
    public interface IModuleClient {
        Task<IEnumerable<ModulePermission>> GetPermissions (ApplicationModule applicationModule);
    }
}