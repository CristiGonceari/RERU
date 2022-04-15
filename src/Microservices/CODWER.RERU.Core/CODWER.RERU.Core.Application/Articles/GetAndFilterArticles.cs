using System.Linq;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Articles
{
    public static class GetAndFilterArticles
    {
        public static IQueryable<ArticleCore> Filter(AppDbContext appDbContext, string name)
        {
            var articles = appDbContext.CoreArticles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                articles = articles.Where(x => x.Name.Contains(name));
            }

            return articles;
        }
    }
}
