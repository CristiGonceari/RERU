using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Evaluation360;

namespace RERU.Data.Persistence.Context
{
    public partial class AppDbContext
    {
        public DbSet<Evaluation> Evaluations {set;get;}
        public virtual DbSet<ArticleEv360> Ev360Articles { get; set; }
        public virtual DbSet<ArticleEv360ModuleRole> ArticleEv360ModuleRoles { get; set; }
    }
}
