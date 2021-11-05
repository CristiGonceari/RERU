using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.DeleteQuestionUnit
{
    public class DeleteQuestionUnitCommandValidator : AbstractValidator<DeleteQuestionUnitCommand>
    {
        private readonly AppDbContext _appDbContext;
        public DeleteQuestionUnitCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(r => r.Id)
                .Must(x => appDbContext.QuestionUnits.Any(d => d.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_QUESTION);

            When(x => appDbContext.TestQuestions.Any(t => t.QuestionUnitId == x.Id), () =>
            {
                RuleFor(x => x.Id)
                    .Must(x => IsQuestionInActiveTest(x) == false)
                    .WithErrorCode(ValidationCodes.QUESTION_IS_IN_ACTIVE_TEST_TYPE);
            });
        }

        private bool IsQuestionInActiveTest(int questionUnitId)
        {
            var tests = _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .Include(x => x.TestType)
                .Where(t => t.TestQuestions.Any(q => q.QuestionUnitId == questionUnitId))
                .ToList();

            foreach (var test in tests)
            {
                if (test.TestType.Status == TestTypeStatusEnum.Active)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
