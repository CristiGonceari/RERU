using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CVU.ERP.Module.Common.Models;
using CVU.ERP.Module.Common.Providers;

namespace CODWER.RERU.Personal.Application.Permissions
{
    public class ModulePermissionProvider : IModulePermissionProvider
    {
        public Task<ModulePermission[]> Get()
        {
            var permissions = new List<ModulePermission>();

            foreach (FieldInfo field in typeof(PermissionCodes).GetFields())
            {
                permissions.Add(new ModulePermission
                {
                    Code = field.GetRawConstantValue().ToString(),
                    Description = field.Name.Replace("_", " ")
                });
            }

            return Task.FromResult(permissions
                .OrderBy(x => x.Code)
                .ToArray());
        }
    }
}
