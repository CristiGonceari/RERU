using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Logging.Entities;
using CVU.ERP.ServiceProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CVU.ERP.Logging.Context
{
    public class LoggingDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public LoggingDbContext()
        {
        }

        public LoggingDbContext(DbContextOptions<LoggingDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Log> Logs { set; get; }
        public virtual DbSet<Article> Articles { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        ///<summary>
        ///Get new instance of LoggingDbContext for thread safe using
        ///</summary>
        //public static LoggingDbContext NewInstance(IConfiguration configuration) => new(new DbContextOptionsBuilder<LoggingDbContext>()
        //    .UseNpgsql(configuration.GetConnectionString(ConnectionString.Logging))
        //    .Options);

        public LoggingDbContext NewInstance()
        {
            return new(new DbContextOptionsBuilder<LoggingDbContext>()
                .UseNpgsql(_configuration.GetConnectionString(ConnectionString.Logging))
                .Options, _configuration);
        }
    }
}
