using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.AddKinshipRelationWithUserProfile
{
    public class AddKinshipRelationWithUserProfileCommandValidator : AbstractValidator<AddKinshipRelationWithUserProfileCommand>
    {
        public AddKinshipRelationWithUserProfileCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new KinshipRelationWithUserProfilesValidator(appDbContext));
        }
    }
}
