using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.UnassignTestTemplateFromEvent
{
    public class UnassignTestTemplateFromEventCommandValidator : AbstractValidator<UnassignTestTemplateFromEventCommand>
    {
        public UnassignTestTemplateFromEventCommandValidator(AppDbContext appDbContext)
        {
                RuleFor(x => x)
                .Must(x => appDbContext.EventTestTemplates.Any(l => l.TestTemplateId == x.TestTemplateId && l.EventId == x.EventId))
                .WithErrorCode(ValidationCodes.INVALID_RECORD);

            RuleFor(x => x.EventId)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));
        }
    }
}
