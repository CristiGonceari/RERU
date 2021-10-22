using CODWER.RERU.Core.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Data.Persistence.Initializer
{
    public class DatabaseSeeder
    {
        public static void Migrate(CoreDbContext coreDbContext)
        {
            coreDbContext.Database.Migrate();
        }

        public static void SeedDb(CoreDbContext appDbContext)
        {
        }
    }
}
