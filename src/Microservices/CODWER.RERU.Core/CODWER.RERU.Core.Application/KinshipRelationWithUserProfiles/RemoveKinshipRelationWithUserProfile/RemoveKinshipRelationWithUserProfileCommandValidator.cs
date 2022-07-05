using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.RemoveKinshipRelationWithUserProfile
{
    public class RemoveKinshipRelationWithUserProfileCommandValidator : AbstractValidator<RemoveKinshipRelationWithUserProfileCommand>
    {
        public RemoveKinshipRelationWithUserProfileCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<KinshipRelationWithUserProfile>(appDbContext, ValidationCodes.KINSHIP_RELATION_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
    
}
