using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetQuestionsPreview
{
    public class GetQuestionsPreviewQueryValidator : AbstractValidator<GetQuestionsPreviewQuery>
    {
        public GetQuestionsPreviewQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleForEach(x => appDbContext.TestTemplates.Include(x => x.TestTemplateQuestionCategories).First(t => t.Id == x.TestTemplateId).TestTemplateQuestionCategories)
                .Must(x => x.QuestionCount <= appDbContext.QuestionCategories.Include(x => x.QuestionUnits).FirstOrDefault(c => c.Id == x.QuestionCategoryId).QuestionUnits.Where(q => q.Status == QuestionUnitStatusEnum.Active).Count())
                .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);
        }
    }
}
