using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.Tests.GetPollResult
{
    public class GetPollResultQueryValidator : AbstractValidator<GetPollResultQuery>
    {
        public GetPollResultQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestTypeId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TYPE,
                    ValidationMessages.InvalidReference));

            RuleFor(r => r.TestTypeId)
                 .Must(x => appDbContext.EventTestTypes.Any(t => t.TestTypeId == x))
                 .WithErrorCode(ValidationCodes.INEXISTENT_POLL_IN_EVENT);

            When(r => !appDbContext.TestTypeSettings.First(x => x.TestTypeId == r.TestTypeId).CanViewPollProgress, () =>
            {
                RuleFor(x => x.TestTypeId)
                .Must(x => appDbContext.EventTestTypes.Include(s => s.Event).First(s => s.TestTypeId == x).Event.TillDate <= DateTime.Now)
                .WithErrorCode(ValidationCodes.POLL_ISNT_TERMINATED);
            });
        }
    }
}

