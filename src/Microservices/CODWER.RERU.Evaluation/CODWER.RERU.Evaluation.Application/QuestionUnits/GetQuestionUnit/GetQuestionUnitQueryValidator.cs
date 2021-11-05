using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionUnit
{
    public class GetQuestionUnitQueryValidator : AbstractValidator<GetQuestionUnitQuery>
    {
        public GetQuestionUnitQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .Must(x => appDbContext.QuestionUnits.Any(q => q.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_QUESTION);
        }
    }
}
