using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Data.Persistence.Initializer
{
    public class DatabaseSeeder
    {
        public static void Migrate(AppDbContext appDbContext)
        {
            appDbContext.Database.Migrate();
        }

        public static void SeedDb(AppDbContext appDbContext)
        {
        }
    }
}
