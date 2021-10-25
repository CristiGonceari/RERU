using System;
using System.Threading.Tasks;
using CODWER.RERU.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SpatialFocus.EntityFrameworkCore.Extensions;

namespace CODWER.RERU.Core.Data.Persistence.Context {
    public partial class CoreDbContext : DbContext {
        public CoreDbContext () { }

        public CoreDbContext (DbContextOptions<CoreDbContext> options):
            base (options) { }

        public DbSet<Module> Modules { set; get; }
        public DbSet<ModulePermission> ModulePermissions { set; get; }
        public DbSet<ModuleRole> ModuleRoles { set; get; }
        public DbSet<ModuleRolePermission> ModuleRolePermissions { set; get; }
        public DbSet<UserProfileModuleRole> UserProfileModuleRoles { set; get; }
        public DbSet<UserProfile> UserProfiles { set; get; }
        public DbSet<DocumentBody> DocumentBodies { set; get; }
        public DbSet<Document> Documents { set; get; }

        protected override void OnConfiguring (
            DbContextOptionsBuilder optionsBuilder
        ) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Module> ().HasKey (c => c.Id);
            modelBuilder.Entity<UserProfile> ().HasKey (c => c.Id);

            modelBuilder
                .ApplyConfigurationsFromAssembly (typeof (CoreDbContext)
                    .Assembly);

            modelBuilder
                .ConfigureEnumLookup (EnumLookupOptions
                    .Default
                    .SetNamingScheme (n => n)
                    .UseNumberAsIdentifier ()
                    .SetDeleteBehavior (DeleteBehavior.Restrict));
        }
    }
}