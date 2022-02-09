using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits
{
    public static class GetAndFilterQuestionUnits
    {
        public static IQueryable<QuestionUnit> Filter(AppDbContext appDbContext, string name, int? categoryId, QuestionTypeEnum? type)
        {
            var questions = appDbContext.QuestionUnits
                .Include(x => x.QuestionCategory)
                .Include(x => x.Options)
                .Include(x => x.TestQuestions)
                .Include(x => x.QuestionUnitTags)
                .ThenInclude(x => x.Tag)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (type != null)
            {
                questions = questions.Where(x => x.QuestionType == type.Value);
            }

            if (categoryId != null)
            {
                questions = questions.Where(x => x.QuestionCategoryId == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                questions = questions.Where(x => x.Question.Contains(name) || x.QuestionUnitTags.Any(qu => qu.Tag.Name.Contains(name)));
            }

            return questions;
        }
    }
}
