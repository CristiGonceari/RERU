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
        private readonly AppDbContext _appDbContext;

        public UnassignTestTemplateFromEventCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x)
                .Must(x => appDbContext.EventTestTemplates.Any(l => l.TestTemplateId == x.TestTemplateId && l.EventId == x.EventId))
                .WithErrorCode(ValidationCodes.INVALID_RECORD);

            RuleFor(x => x.EventId)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x)
                .Must(x => !CheckTestTemplateUses(x.EventId, x.TestTemplateId))
                .WithErrorCode(ValidationCodes.CANT_DELETE_TEST_TEMPLATE_IN_USE);
        }

        private bool CheckTestTemplateUses(int eventId, int testTemplateId) => _appDbContext.Tests.Any(x => x.EventId == eventId && x.TestTemplateId == testTemplateId);
    }
}
