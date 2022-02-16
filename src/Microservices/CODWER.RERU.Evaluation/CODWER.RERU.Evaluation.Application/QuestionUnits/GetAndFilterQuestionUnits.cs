using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits
{
    public static class GetAndFilterQuestionUnits
    {
        public static IQueryable<QuestionUnit> Filter(AppDbContext appDbContext, string questionName, string categoryName, string questionTags, QuestionTypeEnum? type, QuestionUnitStatusEnum? status)
        {
            var questions = appDbContext.QuestionUnits
                .Include(x => x.QuestionCategory)
                .Include(x => x.Options)
                .Include(x => x.TestQuestions)
                .Include(x => x.QuestionUnitTags)
                .ThenInclude(x => x.Tag)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(questionName))
            {
                questions = questions.Where(x => x.Question.Contains(questionName) || x.QuestionUnitTags.Any(qu => qu.Tag.Name.Contains(questionName)));
            }

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                questions = questions.Where(x => x.QuestionCategory.Name.Contains(categoryName));
            }

            if (!string.IsNullOrWhiteSpace(questionTags))
            {
                questions = questions.Where(x => x.QuestionUnitTags.Any(qu => qu.Tag.Name.Contains(questionTags)));
            }

            if (type != null)
            {
                questions = questions.Where(x => x.QuestionType == type.Value);
            }

            if (status != null)
            {
                questions = questions.Where(x => x.Status == status.Value);
            }

            return questions;
        }
    }
}
