using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;

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
                .Select(x => new QuestionUnit
                {
                    Id = x.Id,
                    QuestionType = x.QuestionType,
                    Status = x.Status,
                    Question = x.Question,
                    MediaFileId = x.MediaFileId,
                    Options = x.Options,
                    QuestionCategoryId = x.QuestionCategoryId,
                    QuestionCategory = new QuestionCategory
                    {
                        Id = x.QuestionCategoryId,
                        Name = x.QuestionCategory.Name,
                    },
                    QuestionUnitTags = x.QuestionUnitTags.Select(qut => new QuestionUnitTag
                    {
                        Tag = new Tag
                        {
                            Id = qut.TagId,
                            Name = qut.Tag.Name
                        }
                    }).ToList()
                })
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterData.QuestionName))
            {
                questions = questions.Where(x => x.Question.ToLower().Contains(filterData.QuestionName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filterData.CategoryName))
            {
                questions = questions.Where(x => x.QuestionCategory.Name.ToLower().Contains(filterData.CategoryName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filterData.QuestionTags))
            {
                questions = questions.Where(x => x.QuestionUnitTags.Any(qu => qu.Tag.Name.ToLower().Contains(filterData.QuestionTags.ToLower())));
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
