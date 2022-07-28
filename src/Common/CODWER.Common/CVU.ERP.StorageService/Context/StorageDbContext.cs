using CVU.ERP.Common.Data.Persistence.EntityFramework;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.StorageService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CVU.ERP.StorageService.Context
{
    public class StorageDbContext : ModuleDbContext
    {
        private readonly IConfiguration _configuration;

        public StorageDbContext()
        {
        }

        public StorageDbContext(DbContextOptions<StorageDbContext> options, IConfiguration configuration) 
            : base(options)
        {
            _configuration = configuration;
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

        ///<summary>
        ///Get new instance of StorageDbContext for thread safe using
        ///</summary>
        //public static StorageDbContext NewInstance(IConfiguration configuration, ICurrentApplicationUserProvider currentApplicationUserProvider) 
        //    => new(new DbContextOptionsBuilder<StorageDbContext>()
        //    .UseNpgsql(configuration.GetConnectionString(ConnectionString.Storage))
        //    .Options, currentApplicationUserProvider);

        public StorageDbContext NewInstance()
        {
            return new(new DbContextOptionsBuilder<StorageDbContext>()
                .UseNpgsql(_configuration.GetConnectionString(ConnectionString.Identity))
                .Options, _configuration);
        }
    }
}
