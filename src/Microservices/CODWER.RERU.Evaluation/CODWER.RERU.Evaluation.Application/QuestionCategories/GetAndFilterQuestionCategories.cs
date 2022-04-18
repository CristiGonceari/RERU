using System.Linq;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories
{
    public static class GetAndFilterQuestionCategories
    {
        public static IQueryable<QuestionCategory> Filter(AppDbContext appDbContext, string name)
        {
            var categories = appDbContext.QuestionCategories
                .Include(x => x.QuestionUnits)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                categories = categories.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return categories;
        }
    }
}
