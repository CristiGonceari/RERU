using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Logging.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CVU.ERP.Logging.Context
{
    public partial class LoggingDbContext : DbContext
    {
        public LoggingDbContext()
        {
        }

        public LoggingDbContext(DbContextOptions<LoggingDbContext> options)
            : base(options)
        {
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

        public static LoggingDbContext NewInstance(IConfiguration configuration) => new(new DbContextOptionsBuilder<LoggingDbContext>()
            .UseNpgsql(configuration.GetConnectionString(ConnectionString.Logging))
            .Options);
    }
}
