using System.Linq;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Articles
{
    public static class GetAndFilterArticles
    {
        public static IQueryable<Article> Filter(CoreDbContext appDbContext, string name)
        {
            var articles = appDbContext.Articles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                articles = articles.Where(x => x.Name.Contains(name));
            }

            return articles;
        }
    }
}
