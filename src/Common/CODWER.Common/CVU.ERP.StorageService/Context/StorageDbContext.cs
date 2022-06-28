using CVU.ERP.Common.Data.Persistence.EntityFramework;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.StorageService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CVU.ERP.StorageService.Context
{
    public partial class StorageDbContext : ModuleDbContext
    {
        public StorageDbContext()
        {
        }

        public StorageDbContext(DbContextOptions<StorageDbContext> options) : base(options)
        {
        }

        public virtual DbSet<File> Files { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StorageDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public static StorageDbContext NewInstance(IConfiguration configuration) => new(new DbContextOptionsBuilder<StorageDbContext>()
            .UseNpgsql(configuration.GetConnectionString(ConnectionString.Storage))
            .Options);
    }
}
