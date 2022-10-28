using System.Linq;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;

namespace RERU.Data.Persistence.Context
{
    public partial class AppDbContext
    {
        public DbSet<Module> Modules { set; get; }
        public DbSet<ModulePermission> ModulePermissions { set; get; }
        public DbSet<ModuleRole> ModuleRoles { set; get; }
        public DbSet<ModuleRolePermission> ModuleRolePermissions { set; get; }
        public DbSet<UserProfileModuleRole> UserProfileModuleRoles { set; get; }
        public DbSet<CandidatePosition> CandidatePositions { set; get; }
        public DbSet<UserFile> UserFiles { set; get; }
        public virtual DbSet<ArticleCore> CoreArticles { get; set; }
        public virtual DbSet<ArticleCoreModuleRole> ArticleCoreModuleRoles { get; set; }

        public int GetModuleIdByPrefix(string codePrefixModule)
        {
            return ModuleRolePermissions
                .Include(x => x.Permission)
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Permission.Code.StartsWith(codePrefixModule)).Role.ModuleId;
        }

        public IQueryable<ModuleRolePermission> GetModuleRolePermissionsByPrefix(string codePrefixModule)
        {
            return ModuleRolePermissions
                .Include(x => x.Permission)
                .Include(x => x.Role)
                .Where(x => x.Permission.Code.StartsWith(codePrefixModule));
        }
    }
}
