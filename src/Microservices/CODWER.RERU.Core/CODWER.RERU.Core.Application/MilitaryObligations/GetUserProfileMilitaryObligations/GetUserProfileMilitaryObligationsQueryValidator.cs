using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.MilitaryObligations.GetUserProfileMilitaryObligations
{
    public class GetUserProfileMilitaryObligationsQueryValidator : AbstractValidator<GetUserProfileMilitaryObligationsQuery>
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly AppDbContext _appDbContext;

        public GetUserProfileMilitaryObligationsQueryValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
            _appDbContext = appDbContext;

            RuleFor(x => x.ContractorId)
               .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                   ValidationMessages.InvalidReference));

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
