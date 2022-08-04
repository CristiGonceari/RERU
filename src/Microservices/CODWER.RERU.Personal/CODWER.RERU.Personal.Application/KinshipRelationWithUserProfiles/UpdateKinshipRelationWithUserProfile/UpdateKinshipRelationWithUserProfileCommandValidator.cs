using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.UpdateKinshipRelationWithUserProfile
{
    public class UpdateKinshipRelationWithUserProfileCommandValidator : AbstractValidator<UpdateKinshipRelationWithUserProfileCommand>
    {
        public UpdateKinshipRelationWithUserProfileCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
               .SetValidator(new ItemMustExistValidator<KinshipRelationWithUserProfile>(appDbContext, ValidationCodes.KINSHIP_RELATION_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data)
                .SetValidator(new KinshipRelationWithUserProfilesValidator(appDbContext));
        }
    }
}
