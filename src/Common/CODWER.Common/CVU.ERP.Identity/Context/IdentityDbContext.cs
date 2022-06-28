using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CVU.ERP.Identity.Context
{
    public partial class IdentityDbContext : IdentityDbContext<ERPIdentityUser>
    {
        public IdentityDbContext()
        {
        }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public static IdentityDbContext NewInstance(IConfiguration configuration) => new(new DbContextOptionsBuilder<IdentityDbContext>()
            .UseNpgsql(configuration.GetConnectionString(ConnectionString.Identity))
            .Options);
    }
}
