using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelationWithUserProfile;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles
{
    public class KinshipRelationWithUserProfilesValidator : AbstractValidator<KinshipRelationWithUserProfileDto>
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly AppDbContext _appDbContext;

        public KinshipRelationWithUserProfilesValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
            _appDbContext = appDbContext;

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

            RuleFor(x => x)
               .Must(x => CheckIfCurrentUser(x.ContractorId).Result)
               .WithErrorCode(ValidationCodes.USER_NOT_FOUND);
        }
        private async Task<bool> CheckIfCurrentUser(int id)
        {
            var currentUser = await _currentUserProvider.Get();
            var contractor = _appDbContext.Contractors.Include(up => up.UserProfile).FirstOrDefault(up => up.UserProfileId.ToString() == currentUser.Id);

            if (contractor.Id == id)
            {
                return true;
            }

            return false;
        }
    }
}
