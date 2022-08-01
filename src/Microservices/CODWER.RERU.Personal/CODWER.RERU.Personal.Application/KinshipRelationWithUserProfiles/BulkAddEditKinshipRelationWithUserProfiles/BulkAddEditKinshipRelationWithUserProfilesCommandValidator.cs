using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.BulkAddEditKinshipRelationWithUserProfiles
{
    public class BulkAddEditKinshipRelationWithUserProfilesCommandValidator : AbstractValidator<BulkAddEditKinshipRelationWithUserProfilesCommand>
    {
        public BulkAddEditKinshipRelationWithUserProfilesCommandValidator(AppDbContext appDbContext)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new KinshipRelationWithUserProfilesValidator(appDbContext));
        }
    }
}
