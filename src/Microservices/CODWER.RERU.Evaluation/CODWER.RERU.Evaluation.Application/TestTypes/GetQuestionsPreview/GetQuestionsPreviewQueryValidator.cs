using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetQuestionsPreview
{
    public class GetQuestionsPreviewQueryValidator : AbstractValidator<GetQuestionsPreviewQuery>
    {
        public GetQuestionsPreviewQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTypeId)
                .Must(x => appDbContext.TestTypes.Any(t => t.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);

            RuleForEach(x => appDbContext.TestTypes.Include(x => x.TestTypeQuestionCategories).First(t => t.Id == x.TestTypeId).TestTypeQuestionCategories)
                .Must(x => x.QuestionCount <= appDbContext.QuestionCategories.Include(x => x.QuestionUnits).FirstOrDefault(c => c.Id == x.QuestionCategoryId).QuestionUnits.Where(q => q.Status == QuestionUnitStatusEnum.Active).Count())
                .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);
        }
    }
}
