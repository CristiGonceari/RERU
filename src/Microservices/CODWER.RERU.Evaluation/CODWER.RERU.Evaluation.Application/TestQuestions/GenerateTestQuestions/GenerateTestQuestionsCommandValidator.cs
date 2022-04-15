using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions
{
    public class GenerateTestQuestionsCommandValidator : AbstractValidator<GenerateTestQuestionsCommand>
    {
        public GenerateTestQuestionsCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestId)
                .Must(x => appDbContext.Tests.Include(t => t.TestQuestions).FirstOrDefault(t => t.Id == x).TestQuestions?.Count == 0)
                .WithErrorCode(ValidationCodes.QUESTIONS_ARE_GENERATED_FOR_THIS_TEST);

            RuleForEach(x => appDbContext.Tests
                .Include(x => x.TestTemplate)
                    .ThenInclude(x => x.TestTemplateQuestionCategories)
                .First(t => t.Id == x.TestId)
                .TestTemplate.TestTemplateQuestionCategories)
                .Must(x => x.QuestionCount <= appDbContext.QuestionCategories.Include(x => x.QuestionUnits).FirstOrDefault(c => c.Id == x.QuestionCategoryId).QuestionUnits.Where(q => q.Status == QuestionUnitStatusEnum.Active).Count())
                .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);

        }
    }
}
