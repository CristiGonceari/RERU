using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelationWithUserProfile;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles
{
    public class KinshipRelationWithUserProfilesValidator : AbstractValidator<KinshipRelationWithUserProfileDto>
    {
        public KinshipRelationWithUserProfilesValidator(AppDbContext appDbContext)
        {
            RuleFor(x => (int)x.KinshipDegree)
                .SetValidator(new ExistInEnumValidator<KinshipDegreeEnum>());

            RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_NAME)
            .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.LastName)
           .NotEmpty()
           .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_LAST_NAME)
           .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Function)
           .NotEmpty()
           .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_FUNCTION)
           .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Subdivision)
           .NotEmpty()
           .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_SUBDIVISION)
           .WithMessage(ValidationMessages.InvalidInput);
        }
    }
}
