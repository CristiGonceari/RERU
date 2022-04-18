using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.GetPollResult
{
    public class GetPollResultQueryValidator : AbstractValidator<GetPollResultQuery>
    {
        public GetPollResultQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTemplateId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(r => r.TestTemplateId)
                 .Must(x => appDbContext.EventTestTemplates.Any(t => t.TestTemplateId == x))
                 .WithErrorCode(ValidationCodes.INEXISTENT_POLL_IN_EVENT);

            When(r => !appDbContext.TestTemplateSettings.First(x => x.TestTemplateId == r.TestTemplateId).CanViewPollProgress, () =>
            {
                RuleFor(x => x.TestTemplateId)
                .Must(x => appDbContext.EventTestTemplates.Include(s => s.Event).First(s => s.TestTemplateId == x).Event.TillDate <= DateTime.Now)
                .WithErrorCode(ValidationCodes.POLL_ISNT_TERMINATED);
            });
        }
    }
}

