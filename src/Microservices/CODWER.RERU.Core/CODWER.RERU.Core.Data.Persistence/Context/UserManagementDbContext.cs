using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Identity.Models;
using CVU.ERP.ServiceProvider;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CODWER.RERU.Core.Data.Persistence.Context
{
    public class UserManagementDbContext : IdentityDbContext<ERPIdentityUser>
    {
        private readonly ICurrentApplicationUserProvider _currentApplicationUserProvider;
        private readonly IConfiguration _configuration;

        public UserManagementDbContext()
        {
        }

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options, ICurrentApplicationUserProvider currentApplicationUserProvider, IConfiguration configuration)
            : base(options)
        {
            _currentApplicationUserProvider = currentApplicationUserProvider;
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        ///<summary>
        ///Get new instance of UserManagementDbContext for thread safe using
        ///</summary>
        //public static UserManagementDbContext NewInstance(IConfiguration configuration) => new(new DbContextOptionsBuilder<UserManagementDbContext>()
        //    .UseNpgsql(configuration.GetConnectionString(ConnectionString.Identity))
        //    .Options);

        public UserManagementDbContext NewInstance()
        {
            return new(new DbContextOptionsBuilder<UserManagementDbContext>()
                .UseNpgsql(_configuration.GetConnectionString(ConnectionString.Identity))
                .Options, _currentApplicationUserProvider, _configuration);
        }
    }
}
