using CVU.ERP.ServiceProvider;
using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.AddKinshipRelationWithUserProfile
{
    public class AddKinshipRelationWithUserProfileCommandValidator : AbstractValidator<AddKinshipRelationWithUserProfileCommand>
    {
        public AddKinshipRelationWithUserProfileCommandValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            RuleFor(x => x.Data).SetValidator(new KinshipRelationWithUserProfilesValidator(appDbContext, currentUserProvider));
        }
    }
}
