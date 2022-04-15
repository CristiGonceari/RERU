﻿using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.UnassignPlanEvent
{
    public class UnassignPlanEventCommandValidator : AbstractValidator<UnassignPlanEventCommand>
    {
        public UnassignPlanEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.EventId)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));
        }
    }
}
