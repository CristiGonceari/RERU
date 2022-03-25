using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Articles
{
    public static class GetAndFilterArticles
    {
        public static IQueryable<Article> Filter(AppDbContext appDbContext, string name)
        {
            var articles = appDbContext.Articles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                articles = articles.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return articles;
        }
    }
}
