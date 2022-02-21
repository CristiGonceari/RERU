using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits
{
    public static class GetAndFilterQuestionUnits
    {
        public static IQueryable<QuestionUnit> Filter(AppDbContext appDbContext, QuestionFilterDto filterData)
        {
            var questions = appDbContext.QuestionUnits
                .Include(x => x.QuestionCategory)
                .Include(x => x.Options)
                .Include(x => x.TestQuestions)
                .Include(x => x.QuestionUnitTags)
                .ThenInclude(x => x.Tag)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterData.QuestionName))
            {
                questions = questions.Where(x => x.Question.Contains(filterData.QuestionName) || x.QuestionUnitTags.Any(qu => qu.Tag.Name.Contains(filterData.QuestionName)));
            }

            if (!string.IsNullOrWhiteSpace(filterData.CategoryName))
            {
                questions = questions.Where(x => x.QuestionCategory.Name.Contains(filterData.CategoryName));
            }

            if (!string.IsNullOrWhiteSpace(filterData.QuestionTags))
            {
                questions = questions.Where(x => x.QuestionUnitTags.Any(qu => qu.Tag.Name.Contains(filterData.QuestionTags)));
            }

            if (filterData.Type != null)
            {
                questions = questions.Where(x => x.QuestionType == filterData.Type.Value);
            }

            if (filterData.Status != null)
            {
                questions = questions.Where(x => x.Status == filterData.Status);
            }

            if (filterData.QuestionCategoryId != null)
            {
                questions = questions.Where(x => x.QuestionCategoryId == filterData.QuestionCategoryId);
            }

            return questions;
        }
    }
}
