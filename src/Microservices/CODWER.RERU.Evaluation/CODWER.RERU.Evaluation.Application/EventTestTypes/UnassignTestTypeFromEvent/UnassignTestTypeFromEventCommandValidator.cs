using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes.UnassignTestTypeFromEvent
{
    public class UnassignTestTypeFromEventCommandValidator : AbstractValidator<UnassignTestTypeFromEventCommand>
    {
        public UnassignTestTypeFromEventCommandValidator(AppDbContext appDbContext)
        {
                RuleFor(x => x)
                .Must(x => appDbContext.EventTestTypes.Any(l => l.TestTypeId == x.TestTypeId && l.EventId == x.EventId))
                .WithErrorCode(ValidationCodes.INVALID_RECORD);

            RuleFor(x => x.EventId)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestTypeId)
                .SetValidator(x => new ItemMustExistValidator<TestType>(appDbContext, ValidationCodes.INVALID_TEST_TYPE,
                    ValidationMessages.InvalidReference));
        }
    }
}
