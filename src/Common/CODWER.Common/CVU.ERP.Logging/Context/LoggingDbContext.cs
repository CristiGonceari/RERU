using CVU.ERP.Logging.Entities;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<Log> Logs { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
