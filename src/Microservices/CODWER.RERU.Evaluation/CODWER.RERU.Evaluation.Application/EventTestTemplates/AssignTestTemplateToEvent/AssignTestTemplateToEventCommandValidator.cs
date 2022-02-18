using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.AssignTestTemplateToEvent
{
    public class AssignTestTemplateToEventCommandValidator : AbstractValidator<AssignTestTemplateToEventCommand>
    {
        public AssignTestTemplateToEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.EventId)
                    .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.TestTemplateId)
                    .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data)
                    .Must(x => !appDbContext.EventTestTypes.Any(l => l.TestTemplateId == x.TestTemplateId && l.EventId == x.EventId))
                    .WithErrorCode(ValidationCodes.EXISTENT_TEST_TYPE_IN_EVENT);


                When(r => appDbContext.TestTemplates.First(l => l.Id == r.Data.TestTemplateId).Mode == (int)TestTypeModeEnum.Test, () =>
                {
                    RuleFor(r => r.Data.MaxAttempts)
                    .Must(x => x > 0)
                    .WithErrorCode(ValidationCodes.INVALID_MAX_ATTEMPTS);

                    RuleFor(r => r.Data.EventId)
                    .Must(x => !appDbContext.EventTestTypes.Include(x => x.TestTemplate).Where(e => e.EventId == x).Any(tt => tt.TestTemplate.Mode == TestTypeModeEnum.Poll))
                    .WithErrorCode(ValidationCodes.ONLY_POLLS_OR_TESTS);
                });

                When(r => appDbContext.TestTemplates.First(l => l.Id == r.Data.TestTemplateId).Mode == TestTypeModeEnum.Poll, () =>
                {
                    RuleFor(r => r.Data.EventId)
                    .Must(x => !appDbContext.EventTestTypes.Include(x => x.TestTemplate).Where(e => e.EventId == x).Any(tt => tt.TestTemplate.Mode == (int)TestTypeModeEnum.Test))
                    .WithErrorCode(ValidationCodes.ONLY_POLLS_OR_TESTS);
                });

            });
        }
    }
}
