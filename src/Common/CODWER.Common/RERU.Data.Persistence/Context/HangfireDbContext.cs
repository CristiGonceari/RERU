using Microsoft.EntityFrameworkCore;

namespace RERU.Data.Persistence.Context
{
    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options)
        {
        }
    }
}
