﻿using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;


namespace CODWER.RERU.Evaluation.Application.EventEvaluators.GetAssignedEvaluators
{
    class GetAssignedEvaluatorsQueryValidator : AbstractValidator<GetAssignedEvaluatorsQuery>
    {
        public GetAssignedEvaluatorsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.EventId)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));
        }
    }
}