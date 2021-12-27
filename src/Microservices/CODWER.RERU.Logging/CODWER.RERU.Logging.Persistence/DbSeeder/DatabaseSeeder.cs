using CVU.ERP.Logging.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Logging.Persistence.DbSeeder
{
    public class DatabaseSeeder
    {
        public static void Migrate(LoggingDbContext dbContext)
        {
            dbContext.Database.Migrate();
        }
    }
}
