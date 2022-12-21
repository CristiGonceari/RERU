using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Evaluation360;

namespace RERU.Data.Persistence.Context
{
    public partial class AppDbContext
    {
        public DbSet<Evaluation> Evaluations {set;get;}
    }
}
