using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.AssignTagToQuestionUnit
{
    public class AssignTagToQuestionUnitCommandValidator : AbstractValidator<AssignTagToQuestionUnitCommand>
    {
        public AssignTagToQuestionUnitCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.QuestionUnitId)
                .SetValidator(x => new ItemMustExistValidator<QuestionUnit>(appDbContext, ValidationCodes.INVALID_QUESTION,
                    ValidationMessages.InvalidReference));
        }
    }
}
