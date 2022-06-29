using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CODWER.RERU.Core.Data.Persistence.Context
{
    public partial class UserManagementDbContext : IdentityDbContext<ERPIdentityUser>
    {
        public UserManagementDbContext()
        {
        }

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
            : base(options)
        {
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
        public static UserManagementDbContext NewInstance(IConfiguration configuration) => new(new DbContextOptionsBuilder<UserManagementDbContext>()
            .UseNpgsql(configuration.GetConnectionString(ConnectionString.Identity))
            .Options);
    }
}
