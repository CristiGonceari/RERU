using System.Linq;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Articles
{
    public static class GetAndFilterArticles
    {
        public static IQueryable<ArticleEvaluation> Filter(AppDbContext appDbContext, string name)
        {
            var articles = appDbContext.EvaluationArticles
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                articles = articles.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return articles;
        }
    }
}
