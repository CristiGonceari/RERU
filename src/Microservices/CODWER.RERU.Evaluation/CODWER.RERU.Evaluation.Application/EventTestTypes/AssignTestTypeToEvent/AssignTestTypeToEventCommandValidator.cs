﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes.AssignTestTypeToEvent
{
    public class AssignTestTypeToEventCommandValidator : AbstractValidator<AssignTestTypeToEventCommand>
    {
        public AssignTestTypeToEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.EventId)
                    .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.TestTypeId)
                    .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TYPE,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data)
                    .Must(x => !appDbContext.EventTestTypes.Any(l => l.TestTypeId == x.TestTypeId && l.EventId == x.EventId))
                    .WithErrorCode(ValidationCodes.EXISTENT_TEST_TYPE_IN_EVENT);


                When(r => appDbContext.TestTemplates.First(l => l.Id == r.Data.TestTypeId).Mode == (int)TestTypeModeEnum.Test, () =>
                {
                    RuleFor(r => r.Data.MaxAttempts)
                    .Must(x => x > 0)
                    .WithErrorCode(ValidationCodes.INVALID_MAX_ATTEMPTS);

                    RuleFor(r => r.Data.EventId)
                    .Must(x => !appDbContext.EventTestTypes.Include(x => x.TestType).Where(e => e.EventId == x).Any(tt => tt.TestType.Mode == TestTypeModeEnum.Poll))
                    .WithErrorCode(ValidationCodes.ONLY_POLLS_OR_TESTS);
                });

                When(r => appDbContext.TestTemplates.First(l => l.Id == r.Data.TestTypeId).Mode == TestTypeModeEnum.Poll, () =>
                {
                    RuleFor(r => r.Data.EventId)
                    .Must(x => !appDbContext.EventTestTypes.Include(x => x.TestType).Where(e => e.EventId == x).Any(tt => tt.TestType.Mode == (int)TestTypeModeEnum.Test))
                    .WithErrorCode(ValidationCodes.ONLY_POLLS_OR_TESTS);
                });

            });
        }
    }
}
