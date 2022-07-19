using System;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.Validators.UserProfiles
{
    public class ExistentCurrentUserProfileAndContractor : AbstractValidator<object>
    {
        private readonly IUserProfileService _userProfileService;

        public ExistentCurrentUserProfileAndContractor(IUserProfileService userProfileService, string errorMessage)
        {
            _userProfileService = userProfileService;

            RuleFor(x => x).Custom((data, c) =>
                ExistentUserProfileForCurrentUser(errorMessage, c));
        }

        private void ExistentUserProfileForCurrentUser( string errorMessage, CustomContext context)
        {
            var contractor = _userProfileService.GetCurrentUserProfile().Result;

            var existent = contractor?.Contractor != null;
                
            if (!existent)
            {
                context.AddFail(ValidationCodes.NONEXISTENT_USER_PROFILE, errorMessage);
                throw new Exception(ValidationCodes.NONEXISTENT_USER_PROFILE);
            }
        }
    }
}
