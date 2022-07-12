using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.RegistrationFluxSteps.GetUserProfileRegistrationFluxSteps
{
    public class GetUserProfileRegistrationFluxStepsQueryValidator : AbstractValidator<GetUserProfileRegistrationFluxStepsQuery>
    {
        public GetUserProfileRegistrationFluxStepsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.UserProfileId)
                .SetValidator(new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                    ValidationMessages.InvalidReference));

        }
    }
}
