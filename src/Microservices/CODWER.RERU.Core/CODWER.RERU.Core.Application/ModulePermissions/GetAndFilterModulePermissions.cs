using RERU.Data.Entities;
using System.Linq;

namespace CODWER.RERU.Core.Application.ModulePermissions
{
    public static class GetAndFilterModulePermissions
    {
        public static IQueryable<ModulePermission> Filter(IQueryable<ModulePermission> items, string code, string description)
        {
            if (!string.IsNullOrEmpty(code))
            {
                items = items.Where(p => p.Code.ToLower().Contains(code.ToLower()));
            }

            if (!string.IsNullOrEmpty(description))
            {
                items = items.Where(p => p.Description.ToLower().Contains(description.ToLower()));
            }

            return items;
        }
    }
}
