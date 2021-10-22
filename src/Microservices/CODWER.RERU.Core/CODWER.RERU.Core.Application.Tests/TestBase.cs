using System;
using Microsoft.EntityFrameworkCore;
// using CODWER.RERU.Core.Persistence;
//using CODWER.RERU.Core.Persistence.Models;

namespace CODWER.RERU.Core.Application.Tests {
    public class TestBase {
        //public AppDbContext GetDbContext(bool useSqlLite = false)
        //{
        //    var builder = new DbContextOptionsBuilder<AppDbContext>();
        //    if (useSqlLite)
        //    {
        //        builder.UseSqlite("DataSource=:memory:", x => { });
        //    }
        //    else
        //    {
        //        builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    }

        //    var dbContext = new AppDbContext(builder.Options);
        //    if (useSqlLite)
        //    {
        //        // SQLite needs to open connection to the DB.
        //        // Not required for in-memory-database.
        //        dbContext.Database.OpenConnection();
        //    }

        //    dbContext.Database.EnsureCreated();

        //    return dbContext;
        //}
    }
}