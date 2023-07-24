/*using FluentValidation;
using System;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.AddMyPoll
{
     public class AddMyPollCommandValidator : AbstractValidator<AddMyPollCommand>
    {
        public AddMyPollCommandValidator(AppDbContext appDbContext, IDateTime dateTime)
        {
            RuleFor(x => x.EventId)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.EventId)
                .Must(x => appDbContext.Events.First(e => e.Id == x).FromDate <= dateTime.Now && appDbContext.Events.First(e => e.Id == x).TillDate > dateTime.Now)
                .WithErrorCode(ValidationCodes.FINISHED_EVENT);
        }
    }
}
*/