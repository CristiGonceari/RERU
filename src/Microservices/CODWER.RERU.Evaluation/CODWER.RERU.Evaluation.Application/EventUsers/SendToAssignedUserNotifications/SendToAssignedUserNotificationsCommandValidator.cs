using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventUsers.SendToAssignedUserNotifications
{
    public class SendToAssignedUserNotificationsCommandValidator : AbstractValidator<SendToAssignedUserNotificationsCommand>
    {
        private readonly AppDbContext _appDbContext;
        public SendToAssignedUserNotificationsCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.UserProfileId)
                 .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                     ValidationMessages.InvalidReference));

        }
    }
}
