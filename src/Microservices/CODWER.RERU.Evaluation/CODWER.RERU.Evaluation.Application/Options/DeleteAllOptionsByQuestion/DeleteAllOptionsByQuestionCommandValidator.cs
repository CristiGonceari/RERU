using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Options.DeleteAllOptionsByQuestion
{

    public class DeleteAllOptionsByQuestionCommandValidator : AbstractValidator<DeleteAllOptionsByQuestionCommand>
    {
        private readonly AppDbContext _appDbContext;
        public DeleteAllOptionsByQuestionCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.QuestionUnitId)
                .SetValidator(x => new ItemMustExistValidator<QuestionUnit>(appDbContext, ValidationCodes.INVALID_QUESTION,
                    ValidationMessages.InvalidReference));

            When(x => appDbContext.TestQuestions.Any(t => t.QuestionUnitId == x.QuestionUnitId), () =>
            {
                RuleFor(x => x.QuestionUnitId)
                .Must(x => IsQuestionInActiveTest(x) == false)
                .WithErrorCode(ValidationCodes.QUESTION_IS_IN_ACTIVE_TEST_TEMPLATE);
            });
        }

        private bool IsQuestionInActiveTest(int questionUnitId)
        {
            var tests = _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .Include(x => x.TestTemplate)
                .Where(t => t.TestQuestions.Any(q => q.QuestionUnitId == questionUnitId))
                .ToList();

            foreach (var test in tests)
            {
                if (test.TestTemplate.Status == (int)TestTemplateStatusEnum.Active)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
