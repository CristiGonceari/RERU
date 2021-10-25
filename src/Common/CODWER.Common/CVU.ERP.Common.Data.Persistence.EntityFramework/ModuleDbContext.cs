using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using SpatialFocus.EntityFrameworkCore.Extensions;

namespace CVU.ERP.Common.Data.Persistence.EntityFramework
{
    public class ModuleDbContext : DbContext
    {
        private static MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(ModuleDbContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

        public ModuleDbContext()
        {
        }

        public ModuleDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModuleDbContext).Assembly);

            modelBuilder.ConfigureEnumLookup(EnumLookupOptions.Default.SetNamingScheme(n => n).UseNumberAsIdentifier().SetDeleteBehavior(DeleteBehavior.Restrict));


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType) where TEntity : class
        {
            if (entityType.BaseType == null && typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
            {
                var filterExpression = CreateFilterExpression<TEntity>();

                if (filterExpression != null)
                {
                    if (entityType.IsKeyless)
                    {
                        modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                    }
                    else
                    {
                        modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                    }
                }
            }
        }

        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>() where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> softDeleteFilter = e => false || !((ISoftDeleteEntity)e).IsDeleted;
                expression = softDeleteFilter;
            }

            return expression;
        }

        public override int SaveChanges()
        {
            ApplyTrackingConcept();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyTrackingConcept();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyTrackingConcept()
        {
            ChangeTracker.DetectChanges();

            //Method to get current user
            var currentUserId = "1"; // GetCurrentUser();

            var markedAsDeleted = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);
            var markedAsCreated = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            var markedAsEdited = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);

            if (markedAsDeleted != null)
            {
                foreach (var item in markedAsDeleted)
                {
                    AssignOnDeleting(item, currentUserId);
                }
            }

            if (markedAsCreated != null)
            {
                foreach (var item in markedAsCreated)
                {
                    AssignOnCreation(item, currentUserId);
                    AssignOnEditing(item, currentUserId);
                }
            }

            if (markedAsEdited != null)
            {
                foreach (var item in markedAsEdited)
                {
                    AssignOnEditing(item, currentUserId);
                }
            }
        }

        private void AssignOnCreation(EntityEntry item, string currentUserId)
        {
            if (item.Entity is ITrackingEntity trackingEntity)
            {
                trackingEntity.CreateById = currentUserId;
                trackingEntity.CreateDate = DateTime.Now;
            }
        }

        private void AssignOnEditing(EntityEntry item, string currentUserId)
        {
            if (item.Entity is ITrackingEntity trackingEntity)
            {
                trackingEntity.UpdateById = currentUserId;
                trackingEntity.UpdateDate = DateTime.Now;
            }
        }

        private void AssignOnDeleting(EntityEntry item, string currentUserId)
        {
            if (item.Entity is ISoftDeleteEntity entity)
            {
                AssignOnEditing(item, currentUserId);

                if (entity.IsDeleted == false)
                {
                    // Set the entity to unchanged (if we mark the whole entity as Modified, every field gets sent to Db as an update)
                    item.State = EntityState.Unchanged;
                    // Only update the IsDeleted flag - only this will get sent to the Db
                    entity.IsDeleted = true;
                    entity.DeleteTime = DateTime.Now;
                }
            }
        }
    }
}
