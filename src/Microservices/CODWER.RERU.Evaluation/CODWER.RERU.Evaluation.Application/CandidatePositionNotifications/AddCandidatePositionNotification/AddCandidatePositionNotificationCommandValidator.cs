﻿using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.AddCandidatePositionNotification
{
    public class AddCandidatePositionNotificationCommandValidator : AbstractValidator<AddCandidatePositionNotificationCommand>
    {
        public AddCandidatePositionNotificationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.UserProfileId)
                .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                    ValidationMessages.NotFound));
        }
    }
}