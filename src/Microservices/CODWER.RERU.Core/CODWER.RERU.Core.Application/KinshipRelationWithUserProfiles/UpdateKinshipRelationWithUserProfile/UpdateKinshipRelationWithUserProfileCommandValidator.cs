using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.UpdateKinshipRelationWithUserProfile
{
    public class UpdateKinshipRelationWithUserProfileCommandValidator : AbstractValidator<UpdateKinshipRelationWithUserProfileCommand>
    {
        public UpdateKinshipRelationWithUserProfileCommandValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            RuleFor(x => x.Data.Id)
               .SetValidator(new ItemMustExistValidator<KinshipRelationWithUserProfile>(appDbContext, ValidationCodes.KINSHIP_RELATION_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data)
                .SetValidator(new KinshipRelationWithUserProfilesValidator(appDbContext, currentUserProvider));
        }
    }
}
