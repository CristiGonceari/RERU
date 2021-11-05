using CODWER.RERU.Evaluation.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Data.Persistence.Initializer
{
    public static class DatabaseSeeder
    {
        public static void SeedDb(AppDbContext appDbContext)
        {
            appDbContext.Database.Migrate();
        }
    }
}
