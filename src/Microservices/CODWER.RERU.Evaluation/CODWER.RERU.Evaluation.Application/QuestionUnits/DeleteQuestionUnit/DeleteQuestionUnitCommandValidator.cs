using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.DeleteQuestionUnit
{
    public class DeleteQuestionUnitCommandValidator : AbstractValidator<DeleteQuestionUnitCommand>
    {
        private readonly AppDbContext _appDbContext;
        public DeleteQuestionUnitCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<QuestionUnit>(appDbContext, ValidationCodes.INVALID_QUESTION,
                    ValidationMessages.InvalidReference));

            When(x => appDbContext.TestQuestions.Any(t => t.QuestionUnitId == x.Id), () =>
            {
                RuleFor(x => x.Id)
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
                if (test.TestTemplate.Status == TestTemplateStatusEnum.Active)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
