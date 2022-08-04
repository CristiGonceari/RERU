using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CVU.ERP.Identity.Context
{
    public class IdentityDbContext : IdentityDbContext<ERPIdentityUser>
    {
        private readonly IConfiguration _configuration;

        public IdentityDbContext()
        {
        }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        ///<summary>
        ///Get new instance of IdentityDbContext for thread safe using
        ///</summary>
        //public static IdentityDbContext NewInstance(IConfiguration configuration) => new(new DbContextOptionsBuilder<IdentityDbContext>()
        //    .UseNpgsql(configuration.GetConnectionString(ConnectionString.Identity))
        //    .Options);

        public IdentityDbContext NewInstance()
        {
            return new(new DbContextOptionsBuilder<IdentityDbContext>()
                .UseNpgsql(_configuration.GetConnectionString(ConnectionString.Identity))
                .Options, _configuration);
        }
    }
}
