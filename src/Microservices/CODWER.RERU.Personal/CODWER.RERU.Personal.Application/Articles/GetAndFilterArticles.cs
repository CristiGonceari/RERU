using System.Linq;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.Articles
{
    public static class GetAndFilterArticles
    {
        public static IQueryable<Article> Filter(AppDbContext appDbContext, string name)
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
