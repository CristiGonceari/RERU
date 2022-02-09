using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetQuestionsPreview
{
    public class GetQuestionsPreviewQueryValidator : AbstractValidator<GetQuestionsPreviewQuery>
    {
        public GetQuestionsPreviewQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTypeId)
                .SetValidator(x => new ItemMustExistValidator<Data.Entities.TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleForEach(x => appDbContext.TestTemplates.Include(x => x.TestTypeQuestionCategories).First(t => t.Id == x.TestTypeId).TestTypeQuestionCategories)
                .Must(x => x.QuestionCount <= appDbContext.QuestionCategories.Include(x => x.QuestionUnits).FirstOrDefault(c => c.Id == x.QuestionCategoryId).QuestionUnits.Where(q => q.Status == QuestionUnitStatusEnum.Active).Count())
                .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);
        }
    }
}
