using System.Linq;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Departments
{
    public static class GetAndFilterDepartments
    {
        public static IQueryable<Department> Filter(AppDbContext appDbContext, string name)
        {
            var deparments = appDbContext.Departments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                deparments = deparments.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return deparments;
        }
    }
}
