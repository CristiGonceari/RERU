using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.Options.DeleteOption
{
    public class DeleteOptionCommandValidator : AbstractValidator<DeleteOptionCommand>
    {
        private readonly AppDbContext _appDbContext;
        public DeleteOptionCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Option>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

            When(x => appDbContext.TestQuestions.Any(t => t.QuestionUnitId == appDbContext.Options.Include(x => x.QuestionUnit).FirstOrDefault(o => o.Id == x.Id).QuestionUnit.Id), () =>
            {
                RuleFor(x => x.Id)
                .Must(x => IsQuestionInActiveTest(x) == false)
                .WithErrorCode(ValidationCodes.QUESTION_IS_IN_ACTIVE_TEST_TYPE);
            });
        }

        private bool IsQuestionInActiveTest(int optionId)
        {
            var option = _appDbContext.Options.FirstOrDefault(x => x.Id == optionId);

            var tests = _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .Include(x => x.TestTemplates)
                .Where(t => t.TestQuestions.Any(q => q.QuestionUnitId == option.QuestionUnitId))
                .ToList();

            foreach (var test in tests)
            {
                if (test.TestTemplates.Status == (int)TestTypeStatusEnum.Active)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
