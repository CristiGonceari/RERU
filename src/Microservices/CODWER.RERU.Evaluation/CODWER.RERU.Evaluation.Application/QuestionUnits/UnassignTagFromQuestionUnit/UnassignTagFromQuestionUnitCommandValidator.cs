using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.UnassignTagFromQuestionUnit
{
    public class UnassignTagFromQuestionUnitCommandValidator : AbstractValidator<UnassignTagFromQuestionUnitCommand>
    {
        public UnassignTagFromQuestionUnitCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x)
                .Must(x => appDbContext.QuestionUnitTags.Any(
                    q => q.QuestionUnitId == x.QuestionId && q.TagId == x.TagId))
                .WithErrorCode(ValidationCodes.INVALID_ID);
        }
    }
}
