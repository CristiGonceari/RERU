using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

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
