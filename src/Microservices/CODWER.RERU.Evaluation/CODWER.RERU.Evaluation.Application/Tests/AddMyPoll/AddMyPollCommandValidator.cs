using FluentValidation;
using System;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.Tests.AddMyPoll
{
     public class AddMyPollCommandValidator : AbstractValidator<AddMyPollCommand>
    {
        public AddMyPollCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.EventId)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.EventId)
                .Must(x => appDbContext.Events.First(e => e.Id == x).FromDate <= DateTime.Now && appDbContext.Events.First(e => e.Id == x).TillDate > DateTime.Now)
                .WithErrorCode(ValidationCodes.FINISHED_EVENT);
        }
    }
}
