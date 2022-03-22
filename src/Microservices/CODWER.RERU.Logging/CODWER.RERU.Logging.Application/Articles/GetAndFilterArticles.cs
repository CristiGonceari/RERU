using CVU.ERP.Logging.Context;
using CVU.ERP.Logging.Entities;
using System.Linq;

namespace CODWER.RERU.Logging.Application.Articles
{
    public static class GetAndFilterArticles
    {
        public static IQueryable<Article> Filter(LoggingDbContext appDbContext, string name)
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
