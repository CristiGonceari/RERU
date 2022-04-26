using RERU.Data.Persistence.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Modules
{
    public static class GetAndPrintModules
    {
        public static IQueryable<global::RERU.Data.Entities.Module> Filter(AppDbContext appDbContext, string name)
        {
            var modules = appDbContext.Modules.AsQueryable();

            if (name != null && !string.IsNullOrEmpty(name))
            {
                modules = modules.Where(x => EF.Functions.Like(x.Name, $"%{name}%"));
            }

            return modules;
        }
    }
}
